import { Link } from "react-router-dom";
import './Navbar.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser } from '@fortawesome/free-solid-svg-icons'


const Navbar = () => {
	return (
		<nav className="navbar navbar-expand-lg navbar-dark">
			<div className="container-fluid">
				<Link to='/' className="navbar-brand">Wells Fargo</Link>
				<div className="collapse navbar-collapse" id="navbarNav">
					<ul className="navbar-nav">
						<li className="nav-item rounded">
							<Link to='/' className="nav-link">Home</Link>
						</li>
						<li className="nav-item rounded">
							<Link to='/compare' className="nav-link">Comparison</Link>
						</li>
					</ul>
					<ul className="navbar-nav ms-md-auto gap-2">
						<li className="nav-item dropdown rounded">
							<button 
								className="nav-link dropdown-toggle" id="navbarDropdown" 
								data-bs-toggle="dropdown" aria-expanded="false">
								<FontAwesomeIcon icon={faUser} style={{color: "#ffffff",marginRight: "0.5rem"}} />
								Profile
							</button>
							<ul className="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
								<li><button className="dropdown-item">Account actions</button></li>
								<li>
									<hr className="dropdown-divider" />
								</li>
								<li><button className="dropdown-item"><Link to='/login' className="drop-link">Logout</Link></button></li>
							</ul>
						</li>
					</ul>
				</div>
			</div>
		</nav >
	)
}
export default Navbar;