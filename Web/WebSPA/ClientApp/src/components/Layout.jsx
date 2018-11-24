import React, { Component } from 'react';
import NavMenu from './NavMenu';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';
import { applicationContext } from '../services/securitymgr';
import PadNav from './PadNav'

const styles = {
  sidebar: {
    top: 0,
    bottom: 0,
    left: 0,
    zIndex: 100,
    padding: '48px 0 0',
    // boxShadow: 'inset -1px 0 0 rgba(0, 0, 0, .1)',
    height: '100%'
  },
  content: {
    padding: '48px 0 0'
  }
};


class Layout extends Component {
  render() {
    const { user } = this.props;
    if (!applicationContext.isAuthed(user)) window.location = '/';

    return (
      <div className={'container-fluid'}>
        <nav className={'navbar fixed-top flex-md-nowrap p-0 shadow'}>
          <a className={'navbar-brand col-sm-3 col-md-2 mr-0'}  href="#">
            <img src="http://getbootstrap.com/docs/4.1/assets/brand/bootstrap-solid.svg" width="30" height="30" style={{ 'paddingRight': '5px' }} className="d-inline-block align-top" alt="" />
            PandaLogix</a>
          {/* <input className={'form-control form-control-dark w-100'} /> */}
          {`${user.firstName} ${user.lastName}`}
          <ul className={'navbar-nav px-3'}>
            <li className={'nav-item text-nowrap'}>
              <a className={'nav-link'} onClick={this.logout.bind(this)}>Sign out</a>
            </li>
          </ul>
        </nav>

        <div className={'container-fluid'}>
          <div className={'row'}>
            <div className={!this.props.isPad ? 'col-sm-2' : 'col-sm-1'} style={{ ...styles.sidebar }}>
              {!this.props.isPad ?
                <NavMenu /> :
                <PadNav />}
            </div>
            <div className={!this.props.isPad ? 'col-sm-10' : 'col-sm-11'} style={{ ...styles.content }}>
              {this.props.children}
            </div>
          </div>
        </div>

      </div>
    );
  }

  logout() {
    this.props.actions.logout();
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

export default connect(mapStateToProps, mapDispatchToProps)(Layout);
