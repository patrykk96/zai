import axios from 'axios'

export default () => {
    axios.defaults.baseURL = 'https://localhost:44351/api'
    
    axios.defaults.headers.post = {
        'Content-Type': 'aplication/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };

    axios.defaults.headers.get = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };

    axios.defaults.headers.patch = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };

    axios.defaults.headers.delete = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    };
} 