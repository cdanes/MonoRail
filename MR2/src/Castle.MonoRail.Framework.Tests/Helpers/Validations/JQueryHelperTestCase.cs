// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.MonoRail.Framework.Tests.Helpers.Validations
{
	using Framework.Helpers;
	using NUnit.Framework;
	using Test;

	[TestFixture]
	public class JQueryHelperTestCase
	{
		private JQueryHelper helper;

		[SetUp]
		public void SetUp()
		{
			helper = new JQueryHelper(new StubEngineContext());
		}

		[Test]
		public void ObserverField1()
		{
			var expected = "<script type=\"text/javascript\">" +
				"new Form.Element.Observer('fieldid', 1, " +
				"function(element, value) { jQuery.ajax({url:'update.rails', " +
				"success: function(data){ jQuery('elementtoupdate').html(data);}, data:'value=' + value}) })</script>";

			var actual = helper.ObserveField("fieldid", 1, "update.rails", new DictHelper().CreateDict("update=elementtoupdate"));

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ObserverField2()
		{
			var expected = "<script type=\"text/javascript\">" +
				"new Form.Element.Observer('fieldid', 1, " +
				"function(element, value) { jQuery.ajax({url:'update.rails', " +
				"success: function(data){ jQuery('elementtoupdate').html(data);}, data:obtainvalue()}) })</script>";

			var actual = helper.ObserveField("fieldid", 1, "update.rails", "elementtoupdate", "obtainvalue()");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void OnSuccessFailureCallbacks()
		{
			var expected = "<form  onsubmit=\"jQuery.ajax({url:'something.rails', "
				+ "onSuccess:function(request) { javascriptcode } , "
				+ "onFailure:function(request) { javascriptcode } , "
				+ "data:jQuery('form').serialize();}); return false;\" enctype=\"multipart/form-data\" "
				+ "action=\"something.rails\" method=\"post\" >";

			var actual = helper.BuildFormRemoteTag(new DictHelper().CreateDict("url=something.rails", "onfailure=javascriptcode", "onsuccess=javascriptcode"));

			Assert.AreEqual(expected, actual);
		}
	}
}