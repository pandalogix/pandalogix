import React, { Component } from 'react';
import NavMenu from './NavMenu';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';
import { applicationContext } from '../services/securitymgr';

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
    const { user,history } = this.props;
    if (!applicationContext.isAuthed(user)) window.location='/';

    return (
      <div className={'container-fluid'}>
        <nav className={'navbar fixed-top flex-md-nowrap p-0 shadow'}>
          <a className={'navbar-brand col-sm-3 col-md-2 mr-0'} >PandaLogix</a>
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
            <div className={'col-sm-2'} style={{ ...styles.sidebar }}>
              <NavMenu />
            </div>
            <div className={'col-sm-10'} style={{ ...styles.content }}>
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
