import { UserManager } from './userMgr';
import { PadManager, PadsManager } from './padMgr';

const rootReducer = {
  PadManager: PadManager,
  UserManager: UserManager,
  PadsManager: PadsManager
};
export default rootReducer;