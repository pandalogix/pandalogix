import React, { Component } from 'react'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';
import Layout from './Layout';


class UserProfile extends Component {
  render() {
    const { match, user } = this.props;
    if (match.params.userid === 0) {
      return this.newUserProfile(user);
    }
    return this.existingUserProfile(user);
  }

  newUserProfile(user) {
    return (
      <div>
        new User
        {user.name}
      </div>
    );
  }

  existingUserProfile(user) {
    const username = `${user.firtName} ${user.lastName}`;
    return (
      <Layout>
        <h1>User Profile</h1>
        <div className='container-fluid'>
          <div className='form-group'>
            <label for='userName'>Name</label>
            <input type='text' id='userName' value={username} className='form-control' />
          </div>
          
          <div className='form-group'>
            <label for='userEmail'>Email</label>
            <input type='email' id='userEmail' value={user.email} className='form-control' />
          </div>

          <div className='form-group'>
            <label for='apiKey'>Api Key</label>
            <input type='text' id='apiKey' value={user.apiKey} className='form-control' />
          </div>
          <button className='btn btn-primary' onClick={this.regenerate} >ReGenerate</button>

        </div>

      </Layout>

    )
  }
  regenerate(){

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

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);

