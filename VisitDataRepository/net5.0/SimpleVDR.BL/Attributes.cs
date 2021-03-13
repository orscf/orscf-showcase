using System;

namespace System.ComponentModel.DataAnnotations {

  [AttributeUsage(AttributeTargets.Property)]
  public class PrincipalAttribute : Attribute {

    public PrincipalAttribute() {
    }

  }

  [AttributeUsage(AttributeTargets.Property)]
  public class DependentAttribute : Attribute {

    public DependentAttribute() {
    }

  }

  [AttributeUsage(AttributeTargets.Property)]
  public class LookupAttribute : Attribute {

    public LookupAttribute() {
    }

  }

  [AttributeUsage(AttributeTargets.Property)]
  public class RefererAttribute : Attribute {

    public RefererAttribute() {
    }

  }

  [AttributeUsage(AttributeTargets.Property)]
  public class SystemInternalAttribute : Attribute {

    public SystemInternalAttribute() {
    }

  }

  [AttributeUsage(AttributeTargets.Property)]
  public class FixedAfterCreationAttribute : Attribute {

    public FixedAfterCreationAttribute() {
    }

  }

}
