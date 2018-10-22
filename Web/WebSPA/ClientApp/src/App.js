import React from 'react';
import { Route } from 'react-router';
import Home from './components/Home';
import Dashboard from './components/Dashboard';
import UserProfile from './components/UserProfile';
import PadList from './components/PadList';
import HistoryList from './components/HistoryList';
import Billing from './components/Billing';
import PadSummary from './components/PadSummary';
import PadDesign from './components/PadDesign';
import PadModel from './components/PadModel';
// import ProtectedRoute from './protectedRoute';


export default () => (
  <div>
    <Route exact path='/' component={Home} />
    <Route path='/dashboard' component={Dashboard} />
    <Route path='/profile/:userid' component={UserProfile} />
    <Route path='/pads' component ={PadList}/>
    <Route path='/history' component={HistoryList}/>
    <Route path='/billing' component={Billing}/>
    <Route path='/pad/:padid/summary' component={PadSummary}/>
    <Route path='/pad/:padid/design' component={PadDesign}/>
    <Route path='/pad/:padid/model' component={PadModel}/>

    {/* <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} /> */}
  </div>
);

