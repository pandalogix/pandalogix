import React, { Component } from 'react';
import Layout from './Layout';
import { connect } from 'react-redux';

class Dashboard extends Component {

  render() {
    const { user, history } = this.props;
    if (!user.id)
      history.push('/');
    return (
      <Layout>
        <p>Dashboard</p>
        {user.lastName}
      </Layout>
    );
  }
}
function mapStateToProps(state, ownProps) {
  var user = state.UserManager;

  return {
    user: user
  };
}

function mapDispatchToProps(dispatch) {
  return {
    //ctions: bindActionCreators(userManagementAction, dispatch)
  };
}
export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);

