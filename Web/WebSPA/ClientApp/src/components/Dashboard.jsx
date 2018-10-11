import React, { Component } from 'react';
import Layout from './Layout';
import { connect } from 'react-redux';
import { applicationContext } from '../services/securitymgr';
import './Dashboard.css'


class Dashboard extends Component {

  render() {
    const { user, history } = this.props;
    if (!applicationContext.isAuthed(user)) history.push('/');
    return (
      <Layout>
        <h1>Dashboard</h1>
        <div class="row mb-3">
          <button class="btn btn-primary pull-right">Create Pad</button>
        </div>
        <div class="row mb-3">
          <div class="col-xl-3 col-sm-6 py-2">
            <div class="card bg-success text-white h-100">
              <div class="card-body bg-success">
                <div class="rotate">
                  <i class="fa fa-user fa-4x"></i>
                </div>
                <h6 class="text-uppercase">Users</h6>
                <h1 class="display-4">134</h1>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 py-2">
            <div class="card text-white bg-danger h-100">
              <div class="card-body bg-danger">
                <div class="rotate">
                  <i class="fa fa-list fa-4x"></i>
                </div>
                <h6 class="text-uppercase">Posts</h6>
                <h1 class="display-4">87</h1>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 py-2">
            <div class="card text-white bg-info h-100">
              <div class="card-body bg-info">
                <div class="rotate">
                  <i class="fa fa-twitter fa-4x"></i>
                </div>
                <h6 class="text-uppercase">Tweets</h6>
                <h1 class="display-4">125</h1>
              </div>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 py-2">
            <div class="card text-white bg-warning h-100">
              <div class="card-body">
                <div class="rotate">
                  <i class="fa fa-share fa-4x"></i>
                </div>
                <h6 class="text-uppercase">Shares</h6>
                <h1 class="display-4">36</h1>
              </div>
            </div>
          </div>
        </div>
        <div class="row placeholders mb-3">
                <div class="col-6 col-sm-3 placeholder text-center">
                    <img src="//placehold.it/200/dddddd/fff?text=1" class="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail"/>
                    <h4>Responsive</h4>
                    <span class="text-muted">Device agnostic</span>
                </div>
                <div class="col-6 col-sm-3 placeholder text-center">
                    <img src="//placehold.it/200/e4e4e4/fff?text=2" class="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail"/>
                    <h4>Frontend</h4>
                    <span class="text-muted">UI / UX oriented</span>
                </div>
                <div class="col-6 col-sm-3 placeholder text-center">
                    <img src="//placehold.it/200/d6d6d6/fff?text=3" class="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail"/>
                    <h4>HTML5</h4>
                    <span class="text-muted">Standards-based</span>
                </div>
                <div class="col-6 col-sm-3 placeholder text-center">
                    <img src="//placehold.it/200/e0e0e0/fff?text=4" class="center-block img-fluid rounded-circle" alt="Generic placeholder thumbnail"/>
                    <h4>Framework</h4>
                    <span class="text-muted">CSS and JavaScript</span>
                </div>
            </div>
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

