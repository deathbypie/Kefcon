import React from 'react';
import { connect } from 'react-redux';
import {
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink
} from 'reactstrap';

class KefconNavbar extends React.Component {
    render() {
        const { user } = this.props;
        return (
            <div>
                <Navbar className="navbar-dark bg-dark">
                    <NavbarBrand href="/">Kefcon
                    </NavbarBrand>
                    <Nav className="ml-auto" >
                        <NavItem>
                            <NavLink href="/events">Events</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink href="/games">Games</NavLink>
                        </NavItem>
                        {user ?
                            <NavItem>
                                <NavLink href="/logout">Logout</NavLink>
                            </NavItem>
                            :
                            <NavItem>
                                <NavLink href="/login">Login</NavLink>
                            </NavItem>
                        }
                    </Nav>
                </Navbar>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { authentication } = state;
    const { user } = authentication;
    return {
        user
    };
}

const connectedNavbar = connect(mapStateToProps)(KefconNavbar);
export { connectedNavbar as KefconNavbar };