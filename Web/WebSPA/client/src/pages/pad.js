import React from "react"
import Layout from "../components/layout"
import SEO from "../components/seo"
import PadDiagram from "../components/padDiagram/PadDiagram"
const IndexPage = () => (
  <Layout>
    <SEO title="Pad" />
    <PadDiagram></PadDiagram>
  </Layout>
)

export default IndexPage
