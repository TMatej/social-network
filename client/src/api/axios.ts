import axios from "axios";

axios.defaults.baseURL = "http://localhost:59709";
axios.defaults.withCredentials = true;

export { axios };
