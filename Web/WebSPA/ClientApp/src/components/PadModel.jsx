import React, { Component } from 'react'
import Layout from './Layout';


export default class PadModel extends Component {
  render() {
    return (
      <Layout isPad={true}>
        <div className="container-fluid">
          <h1>Pad Trigger DataModel</h1>
          <div>

          </div>
          <div className='form-group'>
            <textarea className='form-control' value={`{
  'type': 'object',
  'properties': {
    'name': {'type':'string'},
    'roles': {'type': 'array'}
  }
} `
            } readOnly={true} rows={10} />
          </div>
        </div>
      </Layout>
    );
  }
}