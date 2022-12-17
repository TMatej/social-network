import { baseUrl } from ".";
import axios from "axios";

axios.defaults.baseURL = baseUrl;
axios.defaults.withCredentials = true;

export { axios };
