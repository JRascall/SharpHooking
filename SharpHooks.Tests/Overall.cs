using FluentAssertions;
using Xunit;

namespace Tests
{
    public class Overall
    {
        SharpHooking hooks;

        public Overall()
        {
            hooks = SharpHooking.Create();
        }

        [Fact]
        public void Simple()
        {
            hooks
                .Register("test", () => {});
            var value  = hooks.Call("test");
            value.Should().Be(1);
        }

        [Fact]
        public void Multiple()
        {
            hooks
                .Register("test", () => {})
                .Register("test", () => {});
            var value = hooks.Call("test");
            value.Should().Be(2);
        }

        [Fact]
        public void OneCall()
        {
            hooks
                .Register("test", () => { })
                .Register("test1", () => { });
            var value = hooks.Call("test");
            value.Should().Be(1);
        }

        [Fact]
        public void SimpleWithArgs()
        {
            bool isOne;

            hooks
                .Register("test", (args) =>
                {
                    int t = (int)args[0];
                    isOne = t == 1;
                });

            var value = hooks.Call("test", 1);
            value.Should().Be(1);
            value.Should().NotBe(0);
        }
    }
}
