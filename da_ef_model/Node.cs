using System;
using System.Collections.Generic;

namespace da_ef_model;

public partial class Node
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public virtual Node Parent { get; set; }
    public virtual ICollection<Node> Children { get; set; }


}
