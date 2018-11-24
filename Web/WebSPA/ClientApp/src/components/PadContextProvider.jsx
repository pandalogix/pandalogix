import React, { Component } from 'react';

const SharePadContext = React.createContext({});

export class PadContextProvider extends Component {
  constructor(props) {
    super(props);
    this.state = {
      pad: {
        name: 'test name',
        description: 'test description',
        identifier: '00000000-0000-0000-0000-000000000001'
      }
    }
  }

  componentDidMount() {
    // const { match } = this.props;
    // const id = match.params.padid;
    const id = this.props.padid;
    if (id !== 0) {
      // get pad from server.
    }
  }

  savePad() {

  }

  render() {
    const { children } = this.props;
    return (
      <SharePadContext.Provider
        value={{
          pad: this.state.pad,
          onPadChanged: this.savePad
        }}>
        {children}
      </SharePadContext.Provider>

    );
  }
}

export const PadContextConsumer = SharePadContext.Consumer;