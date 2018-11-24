import {
  persistStore,
  persistReducer
} from 'redux-persist';
import storageSession from 'redux-persist/lib/storage/session'
import autoMergeLevel2 from 'redux-persist/lib/stateReconciler/autoMergeLevel2';


const persistConfig = {
  key: 'root',
  storage: storageSession,
  stateReconciler: autoMergeLevel2 // see "Merge Process" section for details.
};

export function createPersistReducer(reducer) {
  return persistReducer(persistConfig, reducer);
}

export function createPersistStore(store) {
  return persistStore(store);
}