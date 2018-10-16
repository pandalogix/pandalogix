import React, { Component } from 'react'

export default class PadLayout extends Component {
  render() {
    return (
      <div>
        {this.props.children}
      </div>
    );
  }
}