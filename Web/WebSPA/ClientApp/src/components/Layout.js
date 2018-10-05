import React from 'react';
import NavMenu from './NavMenu';
import { Link } from 'react-router-dom';


const styles={
  sidebar:{
    top: 0,
    bottom: 0,
    left: 0,
    zIndex: 100,
    padding: '48px 0 0',
    boxShadow: 'inset -1px 0 0 rgba(0, 0, 0, .1)',
    height:'100%'
    },
  content:{
    padding: '48px 0 0'
  }
};


export default props => (
  <div className={'container-fluid'}>
    <nav className={'navbar navbar-dark fixed-top bg-dark flex-md-nowrap p-0 shadow'}>
      <a className={'navbar-brand col-sm-3 col-md-2 mr-0'} >Company name</a>
      {/* <input className={'form-control form-control-dark w-100'} /> */}
      <ul className={'navbar-nav px-3'}>
        <li className={'nav-item text-nowrap'}>
          <a className={'nav-link'}>Sign out</a>
        </li>
      </ul>
    </nav>

    <div className={'container-fluid'}>
      <div className={'row'}>
        <div className={'col-sm-3'} style={{...styles.sidebar}}>
          <NavMenu />
        </div>
        <div className={'col-sm-9'} style={{...styles.content}}>
          {props.children}
        </div>
      </div>
    </div>

  </div>
);
