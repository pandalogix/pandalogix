import React, { Component } from 'react';
import './NavMenu.css';
import { connect } from 'react-redux';

class NavMenu extends Component {
  render() {
    const { user, location } = this.props;
    return (
      <ul className="nav flex-column">
        <li className="nav-item">
          <a className={this.isActive(location, '/dashboard')} href="/dashboard">
            <i className="fal fa-tachometer fa-2x"></i>
            Dashboard <span className="sr-only">(current)</span>
          </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/profile')} href={'/profile/' + user.id}>
            <i className="fal fa-user fa-2x"></i>
            Profile
                </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/pads')} href={'/pads'}>
            <i className="fal fa-list fa-2x"></i>
            Pads
                </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/history')} href={'/history'}>
            <i className="fal fa-history fa-2x"></i>
            History
                </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/billing')} href={'/billing'}>
            <i className="fal fa-credit-card " ></i>
            Billing
                </a>
        </li>
      </ul>

    );
  }
  isActive(location, path) {
    if (location.indexOf(path) !== -1) {
      return 'nav-link active';
    }
    return 'nav-link'
  }

}
function mapStateToProps(state, ownProps) {
  const user = state.UserManager;
  const location = state.routing.location.pathname;
  return {
    user: user,
    location: location
  };
}

export default connect(mapStateToProps, null)(NavMenu);
