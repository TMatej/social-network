import axios from "axios";

axios.defaults.baseURL = import.meta.env.DEV
  ? "http://localhost:5000"
  : "https://social-network-application-server.dyn.cloud.e-infra.cz/";
axios.defaults.withCredentials = true;

export { axios };
