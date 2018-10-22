import React, { Component } from 'react'
import Layout from './Layout';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as padAction from '../actions/padActions';
import PadService from '../services/padService';
class PadSummary extends Component {
  componentDidMount() {
    const { match } = this.props;
    const id = match.params.padid;
    if (!this.props.pad.id) {
      this.props.actions.retreivePad(id);
    }
  }
  render() {
    const { pad } = this.props;
    return (
      <Layout isPad={true}>
        <h1>Summary</h1>
        <button className='btn btn-primary' onClick={this.save.bind(this)}>Save</button>

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


        </div>
      </Layout>
    );
  }

  save() {
    const { pad } = this.props;
    if (pad.id === 0) {
      this.props.actions.createPad(pad);
    } else {
      this.props.actions.updatePad(pad);
    }
  }
}

function mapStateToProps(state, ownProps) {
  var pad = state.PadManager;

  return {
    pad: pad
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(padAction, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(PadSummary);
