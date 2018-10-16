import React, { Component } from 'react';
export default class PadNav extends Component {
  render() {
    return (
      <ul className="nav flex-column">
       <li className="nav-item">
          <a className='nav-link' href="/dashboard">
          <i className="fal fa-arrow-circle-left fa-2x"></i>          </a>
        </li>
        <li className="nav-item">
          <a className='nav-link active' href="/pad/0/summary">
            <i className="fal fa-clipboard fa-2x"></i>
          </a>
        </li>
        <li className="nav-item">
          <a className='nav-link' href="/pad/0/design">
            <i className="fal fa-chalkboard fa-2x"></i>
          </a>
        </li>
      </ul>
    )
  }
}