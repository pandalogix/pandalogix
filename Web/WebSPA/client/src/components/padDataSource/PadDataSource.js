import axio from "axios";

const FetchData = () => {
  return {
    currentMaxSequenceId: 0,
    nodes: [],
    connectors: []
  };
};

const PersistData = data => {
  axio
    .post("http://localhost:5000/api/pad", data)
    .then(() => {
      console.log("success");
    })
    .catch(err => console.error(err));
  return true;
};

export { FetchData, PersistData };
