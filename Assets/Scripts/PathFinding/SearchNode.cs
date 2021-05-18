using UnityEngine;
using System.Collections.Generic;

public class SearchNode {

	public LinkedList<Node> previous;
	public Node current;
	
	public SearchNode(LinkedList<Node> parentList, Node current) {
		
		previous = new LinkedList<Node>(parentList);
		this.current = current;
	}
}
