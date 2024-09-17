import { useState } from 'react'
import { useNavigate } from "react-router-dom";
import './Login.css'

const LoginForm = () => {
	const navigate = useNavigate();
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');

	const handleUsernameChange = (e) => {
		setUsername(e.target.value);
	};

	const handlePasswordChange = (e) => {
		setPassword(e.target.value);
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		console.log('Login submitted');
		console.log('Username:', username);
		console.log('Password:', password);
		setUsername('');
		setPassword('');
	};

	// Print username and password on console
	console.log('Username:', username);
	console.log('Password:', password);

	return (
		<>
			<div id='body' >
				<div className='left'>
					<h1 >Heading</h1>
					<h2>Sub-heading</h2>
				</div>
				<div className='right'>
					<div className='container'>
						<h2 className='font' onClick={() => { navigate("/about") }}>Welcome Back,</h2>
						<form className="login-form" onSubmit={handleSubmit}>

							<div>
								<label htmlFor="username">Username:</label>
								<input
									type="text"
									id="username"
									value={username}
									onChange={handleUsernameChange}
									placeholder='ENTER USERNAME'
								/>
							</div>
							<div>
								<label htmlFor="password">Password:</label>
								<input
									type="password"
									id="password"
									value={password}
									onChange={handlePasswordChange}
									placeholder='ENTER PASSWORD'
								/>
							</div>
							<button type="submit">Login</button>
							<p>In case of any issues, contact Tech Central support team for assistance.</p>
						</form>
					</div>
				</div>
			</div>
			{/* <div id='footer-id'>
        <div className='footer-container'>
          <p>© 1999 - 2023 Wells Fargo. NMLSR ID 399801</p>
        </div>
      </div> */}

		</>
	);
};

export default LoginForm;