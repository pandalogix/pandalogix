import React, { Component } from 'react'
import { connect } from 'react-redux';
import Layout from './Layout';
import PadService from '../services/padService'


class HistoryList extends Component {

  constructor(props) {
    super(props);
    this.state = {
      history: {
        count: 0,
        data: []
      }
    };
  }

  componentDidMount() {
    const { user } = this.props;
    let padService = new PadService(user.identifier);
    padService.getExecutionHistory(1, 25, user.identifier)
      .then(history => {
        this.setState({ history: history });
      });
  }

  render() {
    const { history } = this.state;
    return (
      <Layout>
        <h1>History </h1>
        <div className="">
          <div className="table-responsive">
            <table className="table table-striped">
              <thead className="thead-inverse">
                <tr>
                  <th>#</th>
                  <th>Pad</th>
                  <th>Status</th>
                  <th>Summary</th>
                  <th>Data</th>
                </tr>
              </thead>
              <tbody>
                {
                  history.data.map((h) => {
                    return (
                      <tr>
                        <td>{h.id}</td>
                        <td>{h.padIdentifier}</td>
                        <td>{h.status}</td>
                        <td>{h.executionSummary}</td>
                        <td>{h.createdDate}</td>
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


export default connect(mapStateToProps, null)(HistoryList);

