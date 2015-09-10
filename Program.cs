using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Policy;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel;

namespace Ling473Lab4
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//timer
			var aTimer = new System.Diagnostics.Stopwatch();
			aTimer.Start ();

			//get all targets as list of Strings
			System.IO.StreamReader file = new System.IO.StreamReader ("/home/wlane/Documents/Computational Linguistics/Ling473/Lab4/DNAtargets/targets");
			string line;
			List<String> target_lines = new List<String> ();
			while ((line = file.ReadLine ()) != null) {
				target_lines.Add (line.ToLower());
			}

			//build Trie object with list of target strings
			Trie trie = new Trie (target_lines); //Console.WriteLine ("DEBUG: Trie Built");

			//read in the source text
			String[] docs = Directory.GetFiles("/home/wlane/Documents/Computational Linguistics/Ling473/Lab4/DNAdata/", "*");
			foreach (String doc in docs) {
				Console.WriteLine (doc); 
				trie.getMatches (doc);
				//break; //only for testing!!
			}
			aTimer.Stop ();
			Console.WriteLine ("Program executed in: " + aTimer.Elapsed);
		}
	}

	class Trie
	{
		TrieNode root;
		TrieNode currentNode;

		public Trie(List<String> targets_in )
		{
			this.root = new TrieNode ('r'); // r means root
			this.currentNode = root;

			//build trie with target lines
			foreach(String line in targets_in)
			{
				foreach (char ch in line) {
					//if current node doesnt have the character, add it and traverse
					if (!currentNode.hasChildByID (ch)) {
						currentNode.addChild (ch);
						currentNode = currentNode.getChildByID (ch);
					}
					// else traverse the matching char branch
					else {
						currentNode = currentNode.getChildByID (ch);
					}
				}
				currentNode = root;
			}
		}

		public void setCurrentNode(TrieNode current_in)
		{
			this.currentNode = current_in;
		}
		public TrieNode getCurrentNode()
		{
			return currentNode;
		}
		public void getMatches(String source_in)
		{
			String contents; //String contents = File.ReadAllText (source_in);
			using (StreamReader streamReader = new StreamReader(source_in, Encoding.UTF8))
			{
				contents = streamReader.ReadToEnd();
			}

			for(int i =0; i < contents.Length; i ++)
			{
				currentNode = root;
				for (int j = i; j < contents.Length; j++) {
					char ch = contents [j]; //.ToString ().ToLower ()[0];
					if (currentNode.hasChildByID (ch)) {
						currentNode = currentNode.getChildByID (ch);
						if (!currentNode.hasAnyChildren ()) {
							Console.WriteLine ("\t"+ i + "\t"+ contents.Substring(i,j-i+1));
						}
					} else {
						break;
					}
				}
			}
		}
	}
	class TrieNode
	{
		/* 
			0 = t
			1 = c
			2 = a
			3 = g
			4 = r
		*/
		private char nodeID;
		private TrieNode[] childNodes;

		public TrieNode(char id_in)
		{
			this.nodeID = id_in;
			this.childNodes = new TrieNode[5];
		}

		public char getNodeID()
		{
			return this.nodeID;
		}
		public void addChild(char childId_in)
		{
			switch (childId_in) {
			case 't':
				this.childNodes [0] = new TrieNode (childId_in);
				break;
			case 'T':
				this.childNodes [0] = new TrieNode (childId_in);
				break;
			case 'c':
				this.childNodes [1] = new TrieNode (childId_in);
				break;
			case 'C':
				this.childNodes [1] = new TrieNode (childId_in);
				break;
			case 'a':
				this.childNodes [2] = new TrieNode (childId_in);
				break;
			case 'A':
				this.childNodes [2] = new TrieNode (childId_in);
				break;
			case 'g':
				this.childNodes [3] = new TrieNode (childId_in);
				break;
			case 'G':
				this.childNodes [3] = new TrieNode (childId_in);
				break;
			case 'r':
				this.childNodes [4] = new TrieNode (childId_in);
				break;
			}
		}
		public bool hasChildByID(char childId_in)
		{
			switch (childId_in) {
			case 't':
				if (this.childNodes [0] != null) {
					return true;
				} 
				break;
			case 'c':
				if (this.childNodes [1] != null) {
					return true;
				} 
				break;
			case 'a':
				if (this.childNodes [2] != null) {
					return true;
				} 
				break;
			case 'g':
				if (this.childNodes [3] != null) {
					return true;
				} 
				break;
			case 'T':
				if (this.childNodes [0] != null) {
					return true;
				} 
				break;
			case 'C':
				if (this.childNodes [1] != null) {
					return true;
				} 
				break;
			case 'A':
				if (this.childNodes [2] != null) {
					return true;
				} 
				break;
			case 'G':
				if (this.childNodes [3] != null) {
					return true;
				} 
				break;
			case 'r':
				if (this.childNodes [4] != null) {
					return true;
				} 
				break;
			default:
				return false;
			}
			return false;
		}
		public TrieNode getChildByID(char childId_in)
		{
			switch (childId_in) {
			case 't':
				if (this.childNodes [0] != null) {
					return this.childNodes [0];
				} 
				break;
			case 'c':
				if (this.childNodes [1] != null) {
					return this.childNodes [1];
				}
				break;
			case 'a':
				if (this.childNodes [2] != null) {
					return this.childNodes [2];
				} 
				break;
			case 'g':
				if (this.childNodes [3] != null) {
					return this.childNodes [3];
				} 
				break;
			case 'T':
				if (this.childNodes [0] != null) {
					return this.childNodes [0];
				} 
				break;
			case 'C':
				if (this.childNodes [1] != null) {
					return this.childNodes [1];
				}
				break;
			case 'A':
				if (this.childNodes [2] != null) {
					return this.childNodes [2];
				} 
				break;
			case 'G':
				if (this.childNodes [3] != null) {
					return this.childNodes [3];
				} 
				break;
			case 'r':
				if (this.childNodes [4] != null) {
					return this.childNodes [4];
				} 
				break;
			default:
				return null;
			}
			return null;
		}
		public bool hasAnyChildren()
		{
			foreach (TrieNode n in childNodes) {
				if (n != null) {
					return true;
				}
			}
			return false;
		}
	}
}
