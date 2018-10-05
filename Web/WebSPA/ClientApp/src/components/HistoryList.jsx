import React, { Component } from 'react'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';
import Layout from './Layout';


class HistoryList extends Component {
  render() {
    const { match, user } = this.props;

    return (
      <Layout>
        <h1>History </h1>
        <div>
          {user.firsName}  {user.lastName}
        </div>
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

export default connect(mapStateToProps, mapDispatchToProps)(HistoryList);

