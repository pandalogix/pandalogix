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
        <div className="row mb-3">
          <button className="btn btn-primary pull-right" onClick={this.createPad.bind(this)}>Create Pad</button>
        </div>
        <div className="row mb-3">
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card bg-success text-white h-100">
              <div className="card-body bg-success">
                <div className="rotate">
                  <i className="fa fa-user fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Users</h6>
                <h1 className="display-4">134</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-danger h-100">
              <div className="card-body bg-danger">
                <div className="rotate">
                  <i className="fa fa-list fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Posts</h6>
                <h1 className="display-4">87</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-info h-100">
              <div className="card-body bg-info">
                <div className="rotate">
                  <i className="fa fa-twitter fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Tweets</h6>
                <h1 className="display-4">125</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-warning h-100">
              <div className="card-body">
                <div className="rotate">
                  <i className="fa fa-share fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Shares</h6>
                <h1 className="display-4">36</h1>
              </div>
            </div>
          </div>
        </div>
        <div className="row placeholders mb-3">
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/dddddd/fff?text=1" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Responsive</h4>
            <span className="text-muted">Device agnostic</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/e4e4e4/fff?text=2" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Frontend</h4>
            <span className="text-muted">UI / UX oriented</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/d6d6d6/fff?text=3" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>HTML5</h4>
            <span className="text-muted">Standards-based</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/e0e0e0/fff?text=4" className="center-block img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Framework</h4>
            <span className="text-muted">CSS and JavaScript</span>
          </div>
        </div>
      </Layout>
    );
  }

  createPad() {
    this.props.history.push('/pad/0/summary');
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

