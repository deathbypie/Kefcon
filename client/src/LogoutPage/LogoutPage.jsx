import React from 'react';
import { connect } from 'react-redux'
import { withRouter } from 'react-router'
import { userActions } from '../_actions';

class LogoutPage extends React.Component {
    componentWillMount() {
        if(this.props.user != null) {
            this.props.dispatch(userActions.logout());
        }
        this.props.history.replace('/login');
    }

    render() {
        return null;
    }
}

function mapStateToProps(state) {
    const { authentication } = state;
    const { user } = authentication;
    return {
        user
    };
}

const connectedLogoutPage = withRouter(connect(mapStateToProps)(LogoutPage));
export { connectedLogoutPage as LogoutPage };