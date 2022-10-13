<template>
  <div class="btn-group-fab" role="group" aria-label="FAB Menu">
    <div>
      <button
        data-toggle="modal"
        data-target="#passwordForm"
        type="button"
        class="btn btn-main btn-primary has-tooltip"
        data-placement="left"
        title="Menu"
      >
        <i class="fa fa-plus"></i>
      </button>
    </div>
  </div>
  <div
    class="modal fade"
    id="passwordForm"
    tabindex="-1"
    role="dialog"
    aria-labelledby="passwordFormLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="passwordFormLabel">Nova Senha</h5>
          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <Form @submit="insert" :validation-schema="schema">
          <div class="modal-body">
            <div class="row">
              <div class="col-lg-4 col-xs-12">
                <div class="form-group">
                  <label for="application">Aplicação</label>
                  <vue3-simple-typeahead
                    ref="appAutocomplete"
                    id="typeahead_id"
                    :items="applications"
                    :minInputLength="1"
                    @selectItem="selectItemEventHandler"
                  >
                  </vue3-simple-typeahead>
                </div>
              </div>
              <div class="col-lg-4 col-xs-12">
                <div class="form-group">
                  <label for="username">Usuario</label>
                  <Field
                    name="username"
                    type="text"
                    class="form-control"
                    v-model="password.username"
                  />
                  <ErrorMessage name="username" class="error-feedback" />
                </div>
              </div>
              <div class="col-lg-4 col-xs-12">
                <div class="form-group">
                  <label for="password">Senha</label>
                  <Field
                    name="password"
                    type="password"
                    class="form-control"
                    v-model="password.password"
                  />
                  <ErrorMessage name="password" class="error-feedback" />
                </div>
              </div>
            </div>
            <div
              class="alert alert-success alert-dismissible"
              v-if="successInsert"
            >
              <a href="#" class="close" data-dismiss="alert" aria-label="close"
                >&times;</a
              >
              <strong>Sucesso!</strong> Senha cadastrada.
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-dismiss="modal"
              @click="clearForm"
            >
              Fechar
            </button>
            <button type="submit" class="btn btn-primary">Salvar</button>
          </div>
        </Form>
      </div>
    </div>
  </div>
  <vue-good-table
    ref="pwtable"
    :columns="columns"
    :rows="rows"
    :group-options="{
      enabled: true,
      collapsable: true,
    }"
    :search-options="{
      enabled: true,
      placeholder: 'Buscar...',
    }"
    :pagination-options="{
      enabled: true,
      mode: 'records',
      rowsPerPageLabel: 'Resultados por pagina',
      nextLabel: 'Proximo',
      prevLabel: 'Anterior',
      ofLabel: 'de',
      allLabel: 'Todos',
    }"
    theme="polar-bear"
    v-on:cell-click="onCellClick"
  >
    <template #table-row="props">
      <span v-if="props.column.field == 'password'">
        <input
          v-if="!props.row.showPassword"
          type="password"
          id="pw"
          :value="props.row.password"
        />
        <input
          v-if="props.row.showPassword"
          type="text"
          id="pw"
          :value="props.row.realPassword"
        />
        <button id="showPw">
          <span id="eye" title="Mostrar Senha">
            <i
              class="fa"
              :class="{
                'fa-eye-slash fa-lg': props.row.showPassword,
                'fa-eye fa-lg': !props.row.showPassword,
              }"
            ></i>
          </span>
        </button>
      </span>
      <span v-else-if="props.column.field == 'actions'">
        <button
          type="button"
          @click="deleteRow(props.row.id)"
          class="btn btn-danger"
        >
          <i class="fa fa-trash"></i>
        </button>
      </span>
      <span v-else>
        {{ props.formattedRow[props.column.field] }}
      </span>
    </template>
  </vue-good-table>
</template>
<script>
import pwService from "@/services/pw-service";
import appService from "@/services/app-service";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
  data() {
    const schema = yup.object().shape({
      username: yup.string().required("Usuario é obrigatorio!"),
      password: yup.string().required("Senha é obrigatoria!"),
    });
    return {
      columns: [
        {
          label: "Nome",
          field: "name",
        },
        {
          label: "Usuario",
          field: "username",
        },
        {
          label: "Senha",
          field: "password",
          html: true,
        },
        {
          label: "Ações",
          field: "actions",
          html: true,
        },
      ],
      rows: [],
      schema,
      password: {},
      applications: [],
      successInsert: false,
    };
  },
  methods: {
    onCellClick(params) {
      if (!params.row.showPassword) params.row.showPassword = true;
      else params.row.showPassword = false;
    },
    selectItemEventHandler(params) {
      appService
        .findByName(params)
        .then((response) => (this.password.app_id = response.id));
    },
    deleteRow(id) {
      if (confirm("Deseja mesmo excluir esta senha?")) {
        pwService
          .delete(id)
          .then((response) => {
            console.log(response);
            window.alert("Sucesso");
          })
          .catch((error) => {
            console.log(error);
          })
          .finally(() => this.listAll());
      }
    },
    insert() {
      this.successInsert = false;
      console.log(this.password);
      pwService
        .post(this.password)
        .then((response) => {
          console.log(response);
          this.successInsert = true;
        })
        .catch((error) => {
          console.log(error);
          window.alert(error);
        })
        .finally(() => this.listAll());

      this.clearForm();      
    },
    clearForm() {
      this.password = {};
      this.$refs.appAutocomplete.clearInput();
      this.successInsert = false;
    },
    listAll() {
      pwService.findAllGrouped().then((response) => (this.rows = response));
    },
    listAllAutoComplete() {
      appService
        .findAllAutoComplete()
        .then((response) => (this.applications = response));
    },
  },
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  mounted() {
    this.listAll();
    this.listAllAutoComplete();
  },
};
</script>

<style>
#typeahead_id {
  -webkit-appearance: none;
  background-color: transparent;
  border: 0.1rem solid #d1d1d1;
  border-radius: 0.4rem;
  box-shadow: none;
  box-sizing: inherit;
  padding: 0.6rem 1rem 0.7rem;
  width: 100%;
  height: calc(1.5em + 0.75rem + 2px);
}
#pw {
  border: 0;
  outline: 0;
}
#pw:focus {
  box-shadow: none;
  border-color: #cccccc;
  outline: 0;
  -webkit-box-shadow: none;
  box-shadow: none;
}
#showPw {
  border: 0;
  background: none;
}
.btn-group-fab {
  position: fixed;
  width: 50px;
  height: auto;
  right: 20px;
  bottom: 20px;
}
.btn-group-fab div {
  position: relative;
  width: 100%;
  height: auto;
}
.btn-group-fab .btn {
  position: absolute;
  bottom: 0;
  border-radius: 50%;
  display: block;
  margin-bottom: 4px;
  width: 40px;
  height: 40px;
  margin: 4px auto;
}
.btn-group-fab .btn-main {
  width: 50px;
  height: 50px;
  right: 50%;
  margin-right: -25px;
  z-index: 9;
}
</style>