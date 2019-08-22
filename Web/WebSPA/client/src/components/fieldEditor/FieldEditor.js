import React, { useState } from "react";
import { ComboBoxComponent } from "@syncfusion/ej2-react-dropdowns";
import { SwitchComponent } from "@syncfusion/ej2-react-buttons";

import "./fieldeditor.css";
export default props => {
  const { field, preNodes } = props;
  const [fromNode, setFromNode] = useState(
    !(field.constantValue && field.mappedNodeId === -1)
  );
  const [selectdNode, setSelectdNode] = useState(null);
  const fields = selectdNode
    ? selectdNode.data.fieldsMetaData.map(f => f.name)
    : [];

  const source = `Source ( ${fromNode ? "Node" : "Constant"} )`;
  console.log(field);
  return (
    <div className="form-group" id={field.name}>
      <div className="property-name">{field.name}</div>
      <SwitchComponent
        id={field.name}
        checked={fromNode}
        change={e => {
          setFromNode(!fromNode);
        }}
      />{" "}
      <label className="field-source" htmlFor={field.name}>
        {source}
      </label>
      {fromNode && (
        <div>
          <ComboBoxComponent
            key="nodeselector"
            placeholder="select linked node"
            dataSource={preNodes.map(n => n.id)}
            change={arg => {
              const index = preNodes.findIndex(
                n => n.id === arg.itemData.value
              );
              if (index !== -1) {
                setSelectdNode(preNodes[index]);
                field.mappedNodeId = preNodes[index].id;
              }
            }}
            value={field.mappedNodeId === -1 ? "" : field.mappedNodeId}
          />
          <ComboBoxComponent
            key="fieldselector"
            placeholder="select node field"
            dataSource={fields}
            change={arg => {
              field.mappedFieldName = arg.value;
            }}
            value={
              field.mappedFieldName === "__Result__"
                ? ""
                : field.mappedFieldName
            }
          />
        </div>
      )}
      {!fromNode && (
        <input
          type="text"
          className="form-control"
          id={field.name}
          onChange={e => {
            field.constantValue = e.target.value;
          }}
          value={field.constantValue}
        />
      )}
    </div>
  );
};
