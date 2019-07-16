﻿namespace ClaimsTransformation.Language.Parser
{
    public static class ClaimsTransformationSyntax
    {
        static ClaimsTransformationSyntax()
        {
            Property = new Syntax(
                new[]
                {
                    new Syntax(
                        new Token(Terminals.CLAIM, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.TYPE, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.VALUE_TYPE, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.VALUE, TokenChannel.Normal)
                    )
                },
                SyntaxFlags.Any
            );

            BinaryOperator = new Syntax(
                new[]
                {
                    new Syntax(
                        new Token(Terminals.EQ, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.NEQ, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.REGEXP_MATCH, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.REGEXP_NOT_MATCH, TokenChannel.Normal)
                    ),
                     new Syntax(
                        new Token(Terminals.CONCAT, TokenChannel.Normal)
                    ),
                    new Syntax(
                        new Token(Terminals.ASSIGN, TokenChannel.Normal)
                    )
                },
                SyntaxFlags.Any
            );

            String = new Syntax(
                new Token(Terminals.STRING, TokenChannel.Normal, TokenFlags.String)
            );

            Number = new Syntax(
                new Token(Terminals.NUMBER, TokenChannel.Normal, TokenFlags.Number)
            );

            Boolean = new Syntax(
                new Token(Terminals.BOOLEAN, TokenChannel.Normal, TokenFlags.Boolean)
            );

            Identifier = new Syntax(
                new Token(Terminals.IDENTIFIER, TokenChannel.Normal, TokenFlags.Identifier)
            );

            IdentifierProperty = new Syntax(
                new[]
                {
                    String,
                    new Syntax(
                        new[]
                        {
                            Identifier,
                            new Syntax(
                                new Token(Terminals.DOT)
                            ),
                            Property
                        },
                        SyntaxFlags.All
                    ).WithFactory(ExpressionFactory.Property)
                },
                SyntaxFlags.Any
            );

            Value = new Syntax(
                new[]
                {
                    Property,
                    IdentifierProperty,
                    String,
                    Number,
                    Boolean,
                    Identifier,
                },
                SyntaxFlags.Any
            );

            Expression = new Syntax(
                new[]
                {
                    new Syntax(
                        new[]
                        {
                            Value,
                            new Syntax(
                                new[]
                                {
                                    BinaryOperator,
                                    Value
                                },
                                SyntaxFlags.All | SyntaxFlags.Repeat
                            )
                        },
                        SyntaxFlags.All
                    ).WithFactory(ExpressionFactory.Binary),
                    Value
                },
                SyntaxFlags.Any
            );

            Expressions = new Syntax(
                new[]
                {
                    new Syntax(
                        new[]
                        {
                            Expression,
                            new Syntax(
                                new[]
                                {
                                    new Syntax(
                                        new Token(Terminals.COMMA)
                                    ),
                                    Expression
                                },
                                SyntaxFlags.All | SyntaxFlags.Repeat
                            )
                        },
                        SyntaxFlags.All
                    ),
                    Expression
                },
                SyntaxFlags.Any
            );

            Condition = new Syntax(
                new[]
                {
                    new Syntax(
                        new[]
                        {
                            new Syntax(
                                new[]
                                {
                                    new Syntax(
                                        new Token(Terminals.IDENTIFIER, TokenChannel.Normal, TokenFlags.Identifier)
                                    ),
                                    new Syntax(
                                        new Token(Terminals.COLON)
                                    ),
                                },
                                SyntaxFlags.All
                            ),
                            new Syntax(
                                new Token(Terminals.EMPTY)
                            )
                        },
                        SyntaxFlags.Any
                    ),
                    new Syntax(
                        new Token(Terminals.O_SQ_BRACKET)
                    ),
                    new Syntax(
                        new[]
                        {
                            Expressions,
                            new Syntax(
                                new Token(Terminals.EMPTY)
                            )
                        },
                        SyntaxFlags.Any
                    ),
                    new Syntax(
                        new Token(Terminals.C_SQ_BRACKET)
                    )
                },
                SyntaxFlags.All
            ).WithFactory(ExpressionFactory.Condition);

            ConditionOperator = new Syntax(
                new[]
                {
                    new Syntax(
                        new Token(Terminals.AND, TokenChannel.Normal)
                    )
                },
                SyntaxFlags.Any
            );

            Conditions = new Syntax(
                new[]
                {
                    new Syntax(
                        new[]
                        {
                            Condition,
                            new Syntax(
                                new[]
                                {
                                    ConditionOperator,
                                    Condition
                                },
                                SyntaxFlags.All | SyntaxFlags.Repeat
                            )
                        },
                        SyntaxFlags.All
                    ),
                    Condition,
                },
                SyntaxFlags.Any
            );

            Issue = new Syntax(
                new[]
                {
                    new Syntax(
                        new[]
                        {
                            new Syntax(
                                new Token(Terminals.ISSUE, TokenChannel.Normal)
                            ),
                            new Syntax(
                                new Token(Terminals.ADD, TokenChannel.Normal)
                            )
                        },
                        SyntaxFlags.Any
                    ),
                    new Syntax(
                        new Token(Terminals.O_BRACKET)
                    ),
                    Expressions,
                    new Syntax(
                        new Token(Terminals.C_BRACKET)
                    )
                },
                SyntaxFlags.All
            ).WithFactory(ExpressionFactory.Issue);

            Rule = new Syntax(
                new[]
                {
                    Conditions,
                    new Syntax(
                        new Token(Terminals.IMPLY)
                    ),
                    Issue,
                    new Syntax(
                        new Token(Terminals.SEMICOLON)
                    )
                },
                SyntaxFlags.All
            ).WithFactory(ExpressionFactory.Rule);
        }

        public static Syntax Property { get; private set; }

        public static Syntax BinaryOperator { get; private set; }

        public static Syntax String { get; private set; }

        public static Syntax Number { get; private set; }

        public static Syntax Boolean { get; private set; }

        public static Syntax Identifier { get; private set; }

        public static Syntax IdentifierProperty { get; private set; }

        public static Syntax Value { get; private set; }

        public static Syntax Expression { get; private set; }

        public static Syntax Expressions { get; private set; }

        public static Syntax Condition { get; private set; }

        public static Syntax ConditionOperator { get; private set; }

        public static Syntax Conditions { get; private set; }

        public static Syntax Issue { get; private set; }

        public static Syntax Rule { get; private set; }
    }
}
