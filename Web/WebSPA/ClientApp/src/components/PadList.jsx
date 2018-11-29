import React, { Component } from 'react'
import { connect } from 'react-redux';
import PadService from '../services/padService'
import Layout from './Layout';


class PadList extends Component {

  constructor(props) {
    super(props);
    this.state = {
      pads: {
        count: 0,
        data: []
      }
    };
  }

  componentDidMount() {
    const { user } = this.props;
    let padService = new PadService(user.identifier);
    padService.getPads(1, 25, user.identifier)
      .then(pads => {
        this.setState({ pads: pads });
      });
  }

  render() {

    return (
      <Layout>
        <h1>Pad List</h1>
        <div className="">
          <div className="table-responsive">
            <table className="table table-striped">
              <thead className="thead-inverse">
                <tr>
                  <th>#</th>
                  <th>Name</th>
                  <th>Description</th>
                </tr>
              </thead>
              <tbody>
                {
                  this.state.pads.data.map((p) => {
                    return (
                      <tr>
                        <td>
                          <a href={`/pad/${p.id}/summary`}>
                            {p.identifier}
                          </a>
                        </td>
                        <td>{p.name}</td>
                        <td>{p.description}</td>
                      </tr>
                    )

                  })
                }
              </tbody>
            </table>
          </div>
        </div>
      </Layout>

    )
  }
}

function mapStateToProps(state, ownProps) {
  var user = state.UserManager;

  return {
    user: user
  };
}


export default connect(mapStateToProps, null)(PadList);

