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
                  <i className="fa fa-list fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Pads</h6>
                <h1 className="display-4">10</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-info h-100">
              <div className="card-body bg-info">
                <div className="rotate">
                  <i className="fa fa-history fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Executions</h6>
                <h1 className="display-4">219</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-danger h-100">
              <div className="card-body bg-danger">
                <div className="rotate">
                  <i className="fa fa-exclamation-circle fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Error</h6>
                <h1 className="display-4">10</h1>
              </div>
            </div>
          </div>
          <div className="col-xl-3 col-sm-6 py-2">
            <div className="card text-white bg-success h-100">
              <div className="card-body">
                <div className="rotate">
                  <i className="fa fa-thumbs-up fa-4x"></i>
                </div>
                <h6 className="text-uppercase">Success</h6>
                <h1 className="display-4">209</h1>
              </div>
            </div>
          </div>
        </div>
        <div className="row placeholders mb-3">
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/dddddd/fff?text=1" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Pad 1</h4>
            <span className="text-muted">Most Executed Pad</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/e4e4e4/fff?text=2" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Pad 2</h4>
            <span className="text-muted">2nd Executed Pad</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/d6d6d6/fff?text=3" className="mx-auto img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Pad Error</h4>
            <span className="text-muted">Latest Error Execution of Pad</span>
          </div>
          <div className="col-6 col-sm-3 placeholder text-center">
            <img src="//placehold.it/200/e0e0e0/fff?text=4" className="center-block img-fluid rounded-circle" alt="Generic placeholder thumbnail" />
            <h4>Pad Errored</h4>
            <span className="text-muted">Most Errored Pad</span>
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

