import React,{Component} from 'react';
import Layout from './Layout';
import { connect } from 'react-redux';

 class Dashboard extends Component
{
  render(){
    return (
      <Layout>
        <p>Dashboard</p>
      </Layout>
    );
  }
}

export default connect()(Dashboard);