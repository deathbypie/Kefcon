import React from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { alertActions } from '../_actions';
import { PrivateRoute } from '../_components';
import { HomePage } from '../HomePage';
import { LoginPage } from '../LoginPage';
import { LogoutPage } from '../LogoutPage';
import { UserRegisterPage } from '../UserRegisterPage';
import { KefconNavbar } from '../KefconNavbar';
import { NotFound } from '../NotFound';
import { GamePage } from '../GamePage';
import { EventPage } from '../EventPage';
import { ManageEventPage } from '../ManageEventPage';

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
        history.listen((location, action) => {
            // clear alert on location change
            dispatch(alertActions.clear());
        });
    }

    render() {
        const { alert } = this.props;
        return (
            <div>
                <KefconNavbar></KefconNavbar>
                <div className="jumbotron">
                    <div className="container justify-content-center">
                        <div className="col-sm-8 col-sm-offset-2">
                            {alert.message &&
                                <div className={`alert ${alert.type}`}>{alert.message}</div>
                            }
                            <Router history={history}>
                                <div>
                                    <Switch>
                                        <Route path="/register" component={UserRegisterPage} />
                                        <Route path="/login" component={LoginPage} />
                                        <PrivateRoute path="/logout" component={LogoutPage} />
                                        <PrivateRoute exact path="/" component={HomePage} />
                                        <PrivateRoute path="/games" component={GamePage} />
                                        <PrivateRoute path="/events" component={EventPage} />
                                        <PrivateRoute path="/ManageEvent/:id" component={ManageEventPage} />
                                        <Route path="/*" component={NotFound} />
                                    </Switch>
                                </div>
                            </Router>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { alert } = state;
    return {
        alert
    };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 