import React, {useState} from 'react';
import{Link, useHistory} from 'react-router-dom';
import { useAuthContext } from '../AuthContext';
import axios from 'axios';

const Login = () => {
    const [formData, setFormData] = useState({email: '', password: ''});
    const [isValidLogin, setIsValidLogin] = useState(true);
    const {setUser} = useAuthContext();
    const history = useHistory();

    const onTextChange = e => {
        const copy = {...formData};
        copy[e.target.name] = e.target.value;
        setFormData(copy);
    }

    const onFormSubmit = async e => {
        e.preventDefault();
        const {data} = await axios.post('/api/account/login', formData);
        const isValidLogin = !!data;
        setUser(data);
        setIsValidLogin(isValidLogin);
        history.push('/');
    }

    return(
        <div className='row'>
            <div className="col-md-6 offset-3 card card-body bg-light">
                <h3>Login to your account</h3>
                {!isValidLogin && <span className='text-danger'>invalid username/password please try again</span>}
                <form onSubmit={onFormSubmit}>
                    <input type='text' onChange={onTextChange} value={formData.email} name="email" placeholder="email" className='form-control'/>
                    <br/>
                    <input type='password' onChange={onTextChange} value={formData.password} name="password" placeholder="password" className='form-control'/>
                    <br/>
                    <button className='btn btn-primary'>Login</button>
                </form>
                <Link to='/signup'>sign up for a new account</Link>
            </div>
        </div>
    )
}
export default Login;