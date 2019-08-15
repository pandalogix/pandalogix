import React, { useState } from "react";

import {
  DiagramComponent,
  SnapConstraints,
  SelectorConstraints,
  PortVisibility,
  DiagramTools,
  NodeConstraints,
  Node
} from "@syncfusion/ej2-react-diagrams";
import Property from "../property/Property";

import "./paddiagram.css";
import Palette from "../palette/Palette";
import { FetchData } from "../padDataSource/PadDataSource";

let diagramInstance;
const setNodeDefault = (node, id) => {
  node.constraints = NodeConstraints.Default | NodeConstraints.Shadow;
  node.ports = [
    {
      id: "port1",
      offset: { x: 0, y: 0.5 },
      visibility: PortVisibility.Hover,
      shape: "Circle"
    },
    {
      id: "port2",
      offset: { x: 1, y: 0.5 },
      visibility: PortVisibility.Hover,
      shape: "Circle"
    }
  ];
  node.data.nodeId = id;
};

const setConnectorDefault = connector => {
  connector.type = "Bezier";
  connector.style.strokeColor = "#6f409f";
  connector.style.strokeWidth = 2;
  connector.targetDecorator = {
    style: {
      strokeColor: "#6f409f",
      fill: "#6f409f"
    }
  };
};

const onCollectionChanged = (arg, diagram) => {
  if (arg.state === "Changed") {
    if (
      arg.element &&
      arg.element.type === "Bezier" &&
      (!arg.element.sourcePortID ||
        !arg.element.targetPortID ||
        arg.element.sourceID === arg.element.targetID)
    ) {
      setTimeout(() => {
        diagramInstance.remove(arg.element);
      }, 100);
    }
    //check circular
    if (
      arg.element &&
      arg.element.type === "Bezier" &&
      (arg.element.sourcePortID &&
        arg.element.targetPortID &&
        arg.element.sourceID !== arg.element.targetID)
    ) {
      try {
        getProNode(diagram, diagram.getObject(arg.element.targetID));
      } catch (error) {
        setTimeout(() => {
          diagramInstance.remove(arg.element);
        }, 100);
      }
    }
  }
};

const onMouseenter = arg => {
  if (arg.actualObject) {
    diagramInstance.tool = DiagramTools.DrawOnce;
  }
};

const onClick = arg => {
  diagramInstance.select(arg.element);
  diagramInstance.tool = DiagramTools.Default;
};

let data = FetchData();

const onMouserLeave = arg => {
  diagramInstance.tool = DiagramTools.Default;
};

const getProNode = (diagram, node) => {
  if (!diagram || !node || !(node instanceof Node)) return null;
  let nodeIds = [];
  getProNodeRecusive(
    diagram,
    node,
    nodeIds,
    id => nodeIds.findIndex(n => n === node.id) !== -1
  );
  if (nodeIds) {
    return nodeIds;
  } else return null;
};

const getProNodeRecusive = (diagram, node, nodes, circularCheck) => {
  for (let i = 0; i < diagram.connectors.length; i++) {
    let c = diagram.connectors[i];
    if (c.targetID === node.id && c.sourceID) {
      if (circularCheck(c.sourceID)) {
        throw new Error("Circular connections");
      }
      if (nodes.findIndex(b => b === c.sourceID) === -1) {
        nodes.push(c.sourceID);
        let parentNodes = getProNode(
          diagram,
          diagram.getObject(c.sourceID),
          nodes,
          circularCheck
        );
        if (parentNodes) {
          parentNodes.forEach(element => {
            if (nodes.findIndex(b => b === element) === -1) {
              nodes.push(element);
            } else {
              console.error("duplicate node - " + element);
            }
          });
        }
      }
    }
  }

  return nodes;
};

export default () => {
  const [selectedNode, setSelectNode] = useState(null);

  return (
    <div className="diagram-main">
      <div className="palette-container">
        <Palette />
      </div>
      <div className="diagram-container">
        <DiagramComponent
          id="padDiagram"
          ref={d => (diagramInstance = d)}
          height="590px"
          snapSettings={{ constraints: SnapConstraints.ShowLines }}
          selectedItems={{
            constraints:
              SelectorConstraints.ConnectorSourceThumb |
              SelectorConstraints.ConnectorTargetThumb |
              SelectorConstraints.UserHandle |
              SelectorConstraints.ToolTip
          }}
          nodes={data.nodes}
          connectors={data.connectors}
          getNodeDefaults={node => {
            if (new Error().stack.indexOf("intDragStop") !== -1) {
              data.currentMaxSequenceId++;
            }
            setNodeDefault(node, data.currentMaxSequenceId);
          }}
          getConnectorDefaults={setConnectorDefault}
          tool={
            DiagramTools.MultipleSelect |
            DiagramTools.SingleSelect |
            DiagramTools.ZoomPan
          }
          drawingObject={{
            id: "connector",
            type: "Bezier"
            // constraints:
            //   ConnectorConstraints.Select |
            //   ConnectorConstraints.Delete |
            //   ConnectorConstraints.Interaction
          }}
          collectionChange={arg => onCollectionChanged(arg, diagramInstance)}
          mouseEnter={onMouseenter}
          mouseLeave={onMouserLeave}
          click={onClick}
          selectionChange={arg => {
            setSelectNode(arg.newValue);
          }}
        />
      </div>
      <div className="property-container">
        <Property
          node={selectedNode}
          preNodes={getProNode(diagramInstance, selectedNode)}
          diagram={diagramInstance}
        />
      </div>

      <button
        onClick={() => {
          console.log(diagramInstance.nodes);
          console.log(diagramInstance.connectors);
        }}
      >
        SAVE
      </button>
    </div>
  );
};
