using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffinityRelationship
{
    class Program
    {
        class ItemAffinityRelationship
        {
            public string Item1 { get; set; }
            public string Item2 { get; set; }

            // we can store threshold as well but in this particular use case i am eliminating it at input level,
            // i will store Item Affinity Relationship only if probability qualifies threshold
        }

        static void Main(string[] args)
        {
            decimal threshold = Convert.ToDecimal(Console.ReadLine()); // to store threshold
            int n = Convert.ToInt32(Console.ReadLine()); // number of Item Affinity Relationships
            List<ItemAffinityRelationship> relationships = new List<ItemAffinityRelationship>(); // to store qualified Item Affinity Relationships
            List<List<string>> affinityClusters = new List<List<string>>(); // to store clusters

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                var itemAffinityRelationship = input.Split(' ');
                if (itemAffinityRelationship.Length < 3)
                    return;
                // store Item Affinity Relationship if probability qualifies threshold
                if (Convert.ToDecimal(itemAffinityRelationship[2]) >= threshold)
                {
                    relationships.Add(new ItemAffinityRelationship()
                    {
                        Item1 = itemAffinityRelationship[0],
                        Item2 = itemAffinityRelationship[1]
                    });
                }
            }


            foreach (var relationship in relationships)
            {
                bool relationshipResolved = false;
                foreach (var affinityCluster in affinityClusters)
                {
                    foreach (var item in affinityCluster)
                    {
                        if (relationship.Item1 == item)
                        {
                            // we found cluster mathing this particular relationship, now check if cluster already has item2
                            bool item2Exist = false;
                            foreach (var item2 in affinityCluster)
                            {
                                if (relationship.Item2 == item2)
                                {
                                    item2Exist = true;
                                    break;
                                }
                            }
                            if (!item2Exist)
                            {
                                // add new item in cluster
                                affinityCluster.Add(relationship.Item2);
                            }
                            relationshipResolved = true;
                            break;
                        }
                    }
                    if (relationshipResolved)
                    {
                        break;
                    }
                }
                if (!relationshipResolved)
                {
                    affinityClusters.Add(new List<string>() { relationship.Item1, relationship.Item2 });
                }
            }

            // Find largest cluster(s)
            List<string> largestAffinityCluster = new List<string>();
            List<List<string>> largestAffinityClusters = new List<List<string>>();
            int tempnumber = 0;
            for (int i = 0; i < affinityClusters.Count; i++)
            {
                if (tempnumber <= affinityClusters[i].Count)
                {
                    if (tempnumber < affinityClusters[i].Count)
                    {
                        largestAffinityCluster = affinityClusters[i];
                    }
                    else if (tempnumber == affinityClusters[i].Count)
                    {
                        // there is tie in cluster, store both cluster
                        largestAffinityClusters.Add(largestAffinityCluster);
                        largestAffinityClusters.Add(affinityClusters[i]);
                        largestAffinityCluster.Clear();
                    }
                    tempnumber = affinityClusters[i].Count;
                }
            }

            // check if there was tie in cluster
            if (largestAffinityCluster.Count == 0)
            {
                foreach (var item in largestAffinityClusters)
                {
                    largestAffinityCluster.AddRange(item);
                }
            }

            // sort items in cluster
            string selectedItem = largestAffinityCluster.FirstOrDefault();
            for (int i = 0; i < largestAffinityCluster.Count; i++)
            {
                if (selectedItem.CompareTo(largestAffinityCluster[i]) > 0)
                {
                    selectedItem = largestAffinityCluster[i];
                }
            }

            Console.WriteLine(selectedItem);
        }
    }
}
