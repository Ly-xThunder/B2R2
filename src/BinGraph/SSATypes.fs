(*
  B2R2 - the Next-Generation Reversing Platform

  Author: Sang Kil Cha <sangkilc@kaist.ac.kr>

  Copyright (c) SoftSec Lab. @ KAIST, since 2016

  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
*)

namespace B2R2.BinGraph

open B2R2.BinIR
open System.Collections.Generic

/// Disassembly-based CFG, where each node contains disassembly code.
type SSACFG = ControlFlowGraph<SSABBlock, CFGEdgeKind>

/// A mapping from an address to a SSACFG vertex.
type SSAVMap = Dictionary<ProgramPoint, Vertex<SSABBlock>>

/// This is a mapping from an edge to a dummy vertex (for external function
/// calls). We first separately create dummy vertices even if they are
/// associated with the same node (address) in order to compute dominance
/// relationships without introducing incorrect paths or cycles. For
/// convenience, we will always consider as a key "a return edge" from a fake
/// vertex to a fall-through vertex.
type FakeVMap = Dictionary<ProgramPoint * ProgramPoint, Vertex<SSABBlock>>

/// Mapping from a variable to a set of defining SSA basic blocks.
type DefSites = Dictionary<SSA.VariableKind, Set<Vertex<SSABBlock>>>

/// Defined variables per node in a SSACFG.
type DefsPerNode = Dictionary<Vertex<SSABBlock>, Set<SSA.VariableKind>>

/// Counter for each variable.
type VarCountMap = Dictionary<SSA.VariableKind, int>

/// Variable ID stack.
type IDStack = Dictionary<SSA.VariableKind, int list>
