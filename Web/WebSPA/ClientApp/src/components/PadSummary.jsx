import React, { Component } from 'react'
import Layout from './Layout';
import { PadContextProvider, PadContextConsumer } from './PadContextProvider';

const Test = (pad) => (
  <div>{pad.name}</div>
);
export default class PadSummary extends Component {

  constructor(props) {
    super(props);
    this.state = {
      padid: 0
    };
  }

  componentDidMount() {
    const { match } = this.props;
    const id = match.params.padid;
    this.setState({ padid: id });
  }
  render() {
    const { onPadChanged, pad } = this.props;
    return (
      <PadContextProvider padid={this.state.padid}>
        <h1> Test</h1>
        {/* <Layout isPad={true}> */}
        <PadContextConsumer>

          {value => (
            <div>
              <h1>test </h1>
              {value.pad.name}
            </div>

          )
          }
          {/* <button className='btn btn-primary' onClick={()=>onPadChanged}>Save</button>

            <div className='container-fluid'>
              <div className='form-group'>
                <label htmlFor='name'>Name</label>
                <input type='text' id='name' value={pad.name} className='form-control' />
              </div>

              <div className='form-group'>
                <label htmlFor='description'>Description</label>
                <textarea id='description' value={pad.description} className='form-control' />
              </div>

              <div className='form-group'>
                <label htmlFor='padId'>Key</label>
                <input type='text' id='padId' value={pad.identifier} className='form-control' readOnly={true} />
              </div>

              <div className='form-group'>
                <label htmlFor='HowTo'>How To</label>
                <textarea type='text' id='HowTo' value={'http request'} className='form-control' readOnly={true} rows={10} />
              </div>
            </div> */}
        </PadContextConsumer>
        {/* </Layout> */}
      </PadContextProvider>

    );
  }



}



