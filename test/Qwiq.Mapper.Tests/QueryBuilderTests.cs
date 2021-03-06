using System;
using System.Linq;

using Microsoft.Qwiq.Mapper.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Should;

namespace Microsoft.Qwiq.Mapper
{
    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_a_query_is_on_a_field_that_is_nullable : GenericQueryBuilderContextSpecification<MockModel>
    {
        public override void When()
        {
            base.When();
            Expected += $" WHERE (([NullableField] = 1) AND ([{CoreFieldRefNames.WorkItemType}] = 'MockWorkItem'))";
            Actual = Query.Where(item => item.NullableField.Value == 1).ToString();
        }

        [TestMethod]
        public void the_value_of_the_field_is_compared()
        {
            Actual.ShouldEqual(Expected);
        }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_a_where_clause_filters_on_a_field_with_no_field_definition_attribute : GenericQueryBuilderContextSpecification<MockModel>
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void an_argument_exception_is_thrown()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Query.Where(item => item.UnmappedProperty != null).ToString();
        }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_a_select_clause_is_used : GenericQueryBuilderContextSpecification<MockModel>
    {
        public override void When()
        {
            base.When();
            Expected += $" WHERE (([{CoreFieldRefNames.WorkItemType}] = 'MockWorkItem'))";
            Actual = Query.Select(item => new { One = item.IntField, Two = item.IntField }).ToString();
        }

        [TestMethod]
        public void the_query_string_contains_all_fields_for_work_item()
        {
            Actual.ShouldEqual(Expected);
        }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_two_select_clauses_are_chained : GenericQueryBuilderContextSpecification<MockModel>
    {
        public override void When()
        {
            base.When();
            Expected += $" WHERE (([{CoreFieldRefNames.WorkItemType}] = 'MockWorkItem'))";
            Actual = Query.Select(item => new { One = item.IntField, Two = item.IntField }).Select(item2 => new { ABC = item2.Two }).ToString();
        }

        [TestMethod]
        public void the_query_string_contains_all_fields_for_work_item()
        {
            Actual.ShouldEqual(Expected);
        }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_a_query_is_against_a_type_with_no_workitemtype_attribute : GenericQueryBuilderContextSpecification<MockModelWithNoType>
    {
        public override void When()
        {
            base.When();
            Expected += " WHERE ((([Id] = 1) AND ([IntField] > 5)))";
            Actual = Query.Where(item => item.Id == 1 && item.IntField > 5).ToString();
        }

        [TestMethod]
        public void it_is_translated_to_a_where_with_no_type_restriction()
        {
            Actual.ShouldEqual(Expected);
        }
    }

    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class when_a_query_is_against_a_type_with_multiple_workitemtype_attributes : GenericQueryBuilderContextSpecification<MockModelMultipleTypes>
    {
        public override void When()
        {
            base.When();
            Expected += $" WHERE (([IntField] > 1) AND ([{CoreFieldRefNames.WorkItemType}] IN ('Baz', 'Buzz', 'Fizz')))";
            Actual = Query.Where(item => item.IntField > 1).ToString();
        }

        [TestMethod]
        public void it_is_translated_to_a_where_with_multiple_type_restriction()
        {
            Actual.ShouldEqual(Expected);
        }
    }

}

