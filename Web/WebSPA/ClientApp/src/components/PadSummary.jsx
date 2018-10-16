import React, { Component } from 'react'
import Layout from './Layout';
export default class PadSummary extends Component {
  render() {
    return (
      <Layout isPad={true}>
        <h1>Summary</h1>
        <div className='container-fluid'>
          <div className='form-group'>
            <label htmlFor='name'>Name</label>
            <input type='text' id='name' value={'Pad Name'} className='form-control' />
          </div>
          
          <div className='form-group'>
            <label htmlFor='description'>Description</label>
            <textarea  id='description' value={'description'} className='form-control' />
          </div>

          <div className='form-group'>
            <label htmlFor='padId'>Api Key</label>
            <input type='text' id='padId' value={'000000-000-0000'} className='form-control' />
          </div>
        </div>
      </Layout>
    );
  }
}