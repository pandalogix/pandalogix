import React, { Component } from 'react'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';
import Layout from './Layout';


class Billing extends Component {
  render() {
    
    return (
      <Layout>
        <h1>Billing</h1>
 
      </Layout>

    )
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
    actions: bindActionCreators(userManagementAction, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Billing);

