import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { Routes, Route } from "react-router-dom";
import { Link } from "react-router-dom";
import useAuth from "../hooks";
import AddRecipePage from "../Pages/AddRecipePage";
import Home from "../Pages/Home";
import Login from "../Pages/Login";
import RecipePage from "../Pages/RecipePage";
import Register from "../Pages/Register";

const NavMenu = () => {
    const auth = useAuth();
    return (
        <div>
            <div>
                <Navbar bg="light" expand="lg">
                    <Container>
                        <Navbar.Brand as={Link} to={`/`}>Книга рецептов</Navbar.Brand>
                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="me-auto">
                                <Nav.Link as={Link} to={`/`}>Домой</Nav.Link>
                                {auth.isLoaded ? 
                                <NavDropdown title={JSON.parse(auth.user).name} id="basic-nav-dropdown">
                                <NavDropdown.Item as={Link} to={`/addpage`}>Добавить рецепт</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item onClick={()=>auth.logOut()}>Выйти</NavDropdown.Item>
                            </NavDropdown>
                                :
                            <Nav.Link as={Link} to={`/login`}>Войти</Nav.Link>
                            }
                                
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </div>
            <div>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={
                        <Login />
                    } />
                    <Route path="/register" element={
                        <Register />
                    } />
                    <Route path='/recipes/:id' element={
                        <RecipePage/> } />
                    <Route path='/addpage' element={<AddRecipePage/>}/>
                </Routes>
            </div>
        </div>
    )
}

export default NavMenu