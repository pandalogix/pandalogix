
const FetchData = () => {
  return {
    currentMaxSequenceId:0,
    nodes: [],
    connectors: []
  };
};

const PersistData = data => {
  return true;
};

export {FetchData, PersistData};