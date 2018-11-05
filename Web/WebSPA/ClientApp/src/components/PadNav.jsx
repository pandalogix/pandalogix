import React, { Component } from 'react';
import { connect } from 'react-redux';
import './NavMenu.css';

class PadNav extends Component {
  render() {
    const {location} = this.props;
    return (
      <ul className="nav flex-column">
        <li className="nav-item">
          <a className='nav-link' href="/dashboard">
            <i className="fal fa-arrow-circle-left fa-2x"></i>
          </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/summary')} href="/pad/0/summary">
            <i className="fal fa-clipboard-prescription fa-2x"></i>
          </a>
        </li>
        <li className="nav-item">
          <a className={this.isActive(location, '/design')} href="/pad/0/design">
            <i className="fal fa-chalkboard fa-2x"></i>
          </a>
        </li>
      </ul>
    )
  }
  isActive(location, path) {
    if (location.indexOf(path) !== -1) {
      return 'nav-link active';
    }
    return 'nav-link'
  }
}
function mapStateToProps(state, ownProps) {
  const location = state.routing.location.pathname;
  return {
    location: location
  };
}

export default connect(mapStateToProps, null)(PadNav);