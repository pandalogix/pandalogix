import React, { Component } from 'react'
import Layout from './Layout';
import PadService from '../services/padService'
import { connect } from 'react-redux';

class PadSummary extends Component {
  constructor(props) {
    super(props);
    this.state = {
      pad: {
        id: 0,
        name: 'test name',
        description: 'test description',
        identifier: '00000000-0000-0000-0000-000000000001',
        triggerData: '{}'
      }
    };

  }

  componentDidMount() {
    const { match } = this.props;
    const id = match.params.padid;
    let padService = new PadService(this.props.user.identifier);
    padService.get(id).then(pad => {
      this.setState({ pad: pad });
    })
    // this.setState({ padid: id });
  }
  onPadChanged() {
    let padService = new PadService(this.props.user.identifier);
    console.log(this.state.pad);
    padService.save(this.state.pad).then(data => {
      debugger
      this.props.history.replace(`/pad/${data.id}/summary`)
      this.setState({ pad: data });
    });
  }

  onValueChanged(event, prop) {
    let nprop = {};
    nprop[prop] = event.target.value;
    this.setState({ pad: Object.assign(this.state.pad, nprop) });
  }
  render() {
    const { pad } = this.state;
    return (
      <Layout isPad={true}>
        <div className='container-fluid'>
          <div className='form-group'>
            <label htmlFor='name'>Name</label>
            <input type='text' id='name' value={pad.name}
              className='form-control' onChange={(event) => this.onValueChanged.apply(this, [event, 'name'])} />
          </div>

          <div className='form-group'>
            <label htmlFor='description'>Description</label>
            <textarea id='description' value={pad.description}
              className='form-control' onChange={(event) => this.onValueChanged.apply(this, [event, 'description'])} />
          </div>

          <div className='form-group'>
            <label htmlFor='padId'>Key</label>
            <input type='text' id='padId' value={pad.identifier} className='form-control' readOnly={true} />
          </div>
          <div className='form-group'>
            <label htmlFor='triggerData'>Trigger</label>
            <textarea type='text' id='triggerData'
              onChange={(event) => this.onValueChanged.apply(this, [event, 'triggerData'])}
              value={pad.triggerData} className='form-control' />
          </div>
          <div className='form-group'>
            <label htmlFor='HowTo'>How To</label>
            <textarea type='text' id='HowTo' value={'http request'} className='form-control' readOnly={true} rows={10} />
          </div>
        </div>
        <button className='btn btn-primary' onClick={this.onPadChanged.bind(this)}>Save</button>
      </Layout>

    );
  }



}
function mapStateToProps(state, ownProps) {
  var user = state.UserManager;

  return {
    user: user
  };
}

export default connect(mapStateToProps, null)(PadSummary);



