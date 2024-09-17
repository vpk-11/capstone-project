import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min';

// Component imports
import Navbar from './components/Navbar/Navbar';
import Login from './components/Login/Login';
import List from './components/ListPage/List';
import Feature from './components/Feature Page/Feature';


function App() {
	return (
		<div className='page'>
			<Router>
				<Navbar />
				<div className='home'>
					<Routes>
						<Route path='/login' exact Component={Login} />
						<Route path='/' Component={List} />
						<Route path='/feature' Component={Feature} />
					</Routes>
				</div>
			</Router>
		</div>
	);
}

export default App;
