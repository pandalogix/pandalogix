import React, { Component } from 'react';
import { LinkContainer } from 'react-router-bootstrap';
// import './NavMenu.css';
import { connect } from 'react-redux';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

class NavMenu extends Component {
  render() {
    const { user } = this.props;
    return (
      <ul class="nav flex-column">
              <li class="nav-item">
                <a class="nav-link active" href="/dashboard">
                <i class="fal fa-tachometer"></i>
                Dashboard <span class="sr-only">(current)</span>
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link active" href={'/profile/'+user.id}>
                <i class="fal fa-user"></i>
                Profile
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link active" href={'/pads'}>
                <i class="fal fa-list"></i>
                Pads
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link active" href={'/history'}>
                <i class="fal fa-history"></i>
                History
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link active" href={'/billing'}>
                <i class="fal fa-credit-card"></i>
                Billing
                </a>
              </li>
            </ul>
      // <Navbar inverse fluid collapseOnSelect fixedTop>
      //   {/* <Navbar.Header>
      //     <Navbar.Brand>
      //       <Link to={'/'}>Panda Logix</Link>
      //     </Navbar.Brand>
      //     <Navbar.Toggle />
      //   </Navbar.Header> */}
      //   <Navbar.Collapse>
      //     <Nav>
      //       <LinkContainer to={'/dashboard'} exact>
      //         <NavItem>
      //           <Glyphicon glyph='home' /> Home
      //       </NavItem>
      //       </LinkContainer>
      //       <LinkContainer to={'/profile/'+user.id}>
      //         <NavItem>
      //           <Glyphicon glyph='user' /> Profile
      //       </NavItem>
      //       </LinkContainer>
      //       <LinkContainer to={'/pads'}>
      //         <NavItem>
      //           <Glyphicon glyph='tasks' /> Pads
      //       </NavItem>
      //       </LinkContainer>
      //       <LinkContainer to={'/history'}>
      //         <NavItem>
      //           <Glyphicon glyph='th-list' /> History
      //       </NavItem>
      //       </LinkContainer>
      //       <LinkContainer to={'/billing'}>
      //         <NavItem>
      //           <Glyphicon glyph='usd' /> Billing
      //       </NavItem>
      //       </LinkContainer>
      //     </Nav>
      //   </Navbar.Collapse>
      // </Navbar>
    );
  }
}
function mapStateToProps(state, ownProps) {
  var user = state.UserManager;

  return {
    user: user
  };
}

export default connect(mapStateToProps, null)(NavMenu);
