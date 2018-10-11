import React, { Component } from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';
import { connect } from 'react-redux';

class NavMenu extends Component {
  render() {
    const { user,location } = this.props;
    return (
      <ul class="nav flex-column">
        <li class="nav-item">
          <a class="nav-link " className={this.isActive(location,'/dashboard')} href="/dashboard">
            <i class="fal fa-tachometer"></i>
            Dashboard <span class="sr-only">(current)</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link " className={this.isActive(location,'/profile')}  href={'/profile/' + user.id}>
            <i class="fal fa-user"></i>
            Profile
                </a>
        </li>
        <li class="nav-item">
          <a class="nav-link " className={this.isActive(location,'/pads')}  href={'/pads'}>
            <i class="fal fa-list"></i>
            Pads
                </a>
        </li>
        <li class="nav-item">
          <a class="nav-link " className={this.isActive(location,'/history')}  href={'/history'}>
            <i class="fal fa-history"></i>
            History
                </a>
        </li>
        <li class="nav-item">
          <a class="nav-link " className={this.isActive(location,'/billing')} href={'/billing'}>
            <i class="fal fa-credit-card"></i>
            Billing
                </a>
        </li>
      </ul>

    );
  }
  isActive(location,path){
    if(location.indexOf(path)!==-1){
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
    location:location
  };
}

export default connect(mapStateToProps, null)(NavMenu);
