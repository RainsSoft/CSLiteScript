/*
    -----------------------------------------------------------------------------
    CSLiteScript is a fast easy Script Language for dotnetframework,Mono,Unity hot fix and everywhere
    This source file is part of CSLiteScript
    For the latest info, see https://github.com/RainsSoft/CSLiteScript/
    my blog:http://blog.csdn.net/andyhebear
  
   
    Copyright (c) 2014-2030 rains soft
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
    -----------------------------------------------------------------------------
    */
namespace CSLiteScript
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal sealed class Code
    {
        public ArrayList arrProc;
        private BreakpointList breakpoint_list;
        public CallStack callstack;
        private int card;
        private bool Checked;
        private ObjectStack checked_stack;
        private int curr_stack_count;
        private CSLite_TypedList custom_ex_list;
        private bool debugging;
        public const int DELTA_PROG_CARD = 0x3e8;
        private Contact_Integers detailed_addition_operators;
        private Contact_Integers detailed_bitwise_and_operators;
        private Contact_Integers detailed_bitwise_complement_operators;
        private Contact_Integers detailed_bitwise_or_operators;
        private Contact_Integers detailed_bitwise_xor_operators;
        private Contact_Integers detailed_dec_operators;
        private Contact_Integers detailed_division_operators;
        private Contact_Integers detailed_eq_operators;
        private Contact_Integers detailed_exponent_operators;
        private Contact_Integers detailed_ge_operators;
        private Contact_Integers detailed_gt_operators;
        private Contact_Integers detailed_inc_operators;
        private Contact_Integers detailed_le_operators;
        private Contact_Integers detailed_left_shift_operators;
        private Contact_Integers detailed_logical_negation_operators;
        private Contact_Integers detailed_lt_operators;
        private Contact_Integers detailed_multiplication_operators;
        private Contact_Integers detailed_ne_operators;
        private Contact_Integers detailed_negation_operators;
        private Contact_Integers detailed_operators;
        private Contact_Integers detailed_remainder_operators;
        private Contact_Integers detailed_right_shift_operators;
        private Contact_Integers detailed_subtraction_operators;
        public const int FIRST_PROG_CARD = 0x3e8;
        private ArrayList get_item_list;
        private int goto_line;
        public const int MAX_PROC = 0x3e8;
        public int n;
        public int OP_ADD_ADD_ACCESSOR;
        public int OP_ADD_ANCESTOR;
        public int OP_ADD_ARRAY_INDEX;
        public int OP_ADD_ARRAY_RANGE;
        public int OP_ADD_DEFAULT_VALUE;
        public int OP_ADD_DELEGATES;
        public int OP_ADD_EVENT_FIELD;
        public int OP_ADD_EXPLICIT_INTERFACE;
        public int OP_ADD_HANDLES;
        public int OP_ADD_IMPLEMENTS;
        public int OP_ADD_INDEX;
        public int OP_ADD_MAX_VALUE;
        public int OP_ADD_MIN_VALUE;
        public int OP_ADD_MODIFIER;
        public int OP_ADD_PARAM;
        public int OP_ADD_PARAMS;
        public int OP_ADD_PATTERN;
        public int OP_ADD_READ_ACCESSOR;
        public int OP_ADD_REMOVE_ACCESSOR;
        public int OP_ADD_UNDERLYING_TYPE;
        public int OP_ADD_WRITE_ACCESSOR;
        public int OP_ADDITION_DECIMAL;
        public int OP_ADDITION_DOUBLE;
        public int OP_ADDITION_FLOAT;
        public int OP_ADDITION_INT;
        public int OP_ADDITION_LONG;
        public int OP_ADDITION_STRING;
        public int OP_ADDITION_UINT;
        public int OP_ADDITION_ULONG;
        public int OP_ADDRESS_OF;
        public int OP_AS;
        public int OP_ASSIGN;
        public int OP_ASSIGN_COND_TYPE;
        public int OP_ASSIGN_NAME;
        public int OP_ASSIGN_STRUCT;
        public int OP_ASSIGN_TYPE;
        public int OP_BEGIN_CALL;
        public int OP_BEGIN_MODULE;
        public int OP_BEGIN_USING;
        public int OP_BITWISE_AND;
        public int OP_BITWISE_AND_BOOL;
        public int OP_BITWISE_AND_INT;
        public int OP_BITWISE_AND_LONG;
        public int OP_BITWISE_AND_UINT;
        public int OP_BITWISE_AND_ULONG;
        public int OP_BITWISE_COMPLEMENT_INT;
        public int OP_BITWISE_COMPLEMENT_LONG;
        public int OP_BITWISE_COMPLEMENT_UINT;
        public int OP_BITWISE_COMPLEMENT_ULONG;
        public int OP_BITWISE_OR;
        public int OP_BITWISE_OR_BOOL;
        public int OP_BITWISE_OR_INT;
        public int OP_BITWISE_OR_LONG;
        public int OP_BITWISE_OR_UINT;
        public int OP_BITWISE_OR_ULONG;
        public int OP_BITWISE_XOR;
        public int OP_BITWISE_XOR_BOOL;
        public int OP_BITWISE_XOR_INT;
        public int OP_BITWISE_XOR_LONG;
        public int OP_BITWISE_XOR_UINT;
        public int OP_BITWISE_XOR_ULONG;
        public int OP_CALL;
        public int OP_CALL_ADD_EVENT;
        public int OP_CALL_BASE;
        public int OP_CALL_SIMPLE;
        public int OP_CALL_VIRT;
        public int OP_CAST;
        public int OP_CATCH;
        public int OP_CHECK_STRUCT_CONSTRUCTOR;
        public int OP_CHECKED;
        public int OP_COMPLEMENT;
        public int OP_CREATE_ARRAY_INSTANCE;
        public int OP_CREATE_CLASS;
        public int OP_CREATE_EVENT;
        public int OP_CREATE_FIELD;
        public int OP_CREATE_INDEX_OBJECT;
        public int OP_CREATE_METHOD;
        public int OP_CREATE_NAMESPACE;
        public int OP_CREATE_OBJECT;
        public int OP_CREATE_PROPERTY;
        public int OP_CREATE_REF_TYPE;
        public int OP_CREATE_REFERENCE;
        public int OP_CREATE_TYPE_REFERENCE;
        public int OP_CREATE_USING_ALIAS;
        public int OP_DEC;
        public int OP_DEC_BYTE;
        public int OP_DEC_CHAR;
        public int OP_DEC_DECIMAL;
        public int OP_DEC_DOUBLE;
        public int OP_DEC_FLOAT;
        public int OP_DEC_INT;
        public int OP_DEC_LONG;
        public int OP_DEC_SBYTE;
        public int OP_DEC_SHORT;
        public int OP_DEC_UINT;
        public int OP_DEC_ULONG;
        public int OP_DEC_USHORT;
        public int OP_DECLARE_LOCAL_SIMPLE;
        public int OP_DECLARE_LOCAL_VARIABLE;
        public int OP_DECLARE_LOCAL_VARIABLE_RUNTIME;
        public int OP_DEFINE;
        public int OP_DISCARD_ERROR;
        public int OP_DISPOSE;
        public int OP_DIV;
        public int OP_DIVISION_DECIMAL;
        public int OP_DIVISION_DOUBLE;
        public int OP_DIVISION_FLOAT;
        public int OP_DIVISION_INT;
        public int OP_DIVISION_LONG;
        public int OP_DIVISION_UINT;
        public int OP_DIVISION_ULONG;
        public int OP_DUMMY;
        public int OP_DYNAMIC_INVOKE;
        public int OP_END_CLASS;
        public int OP_END_METHOD;
        public int OP_END_MODULE;
        public int OP_END_REGION;
        public int OP_END_USING;
        public int OP_EQ;
        public int OP_EQ_BOOL;
        public int OP_EQ_DECIMAL;
        public int OP_EQ_DELEGATES;
        public int OP_EQ_DOUBLE;
        public int OP_EQ_FLOAT;
        public int OP_EQ_INT;
        public int OP_EQ_LONG;
        public int OP_EQ_OBJECT;
        public int OP_EQ_STRING;
        public int OP_EQ_UINT;
        public int OP_EQ_ULONG;
        public int OP_EVAL;
        public int OP_EVAL_BASE_TYPE;
        public int OP_EVAL_TYPE;
        public int OP_EXIT_ON_ERROR;
        public int OP_EXIT_SUB;
        public int OP_EXPLICIT_OFF;
        public int OP_EXPLICIT_ON;
        public int OP_EXPONENT;
        public int OP_EXPONENT_DECIMAL;
        public int OP_EXPONENT_DOUBLE;
        public int OP_EXPONENT_FLOAT;
        public int OP_EXPONENT_INT;
        public int OP_EXPONENT_LONG;
        public int OP_EXPONENT_UINT;
        public int OP_EXPONENT_ULONG;
        public int OP_FALSE;
        public int OP_FINALLY;
        public int OP_FIND_FIRST_DELEGATE;
        public int OP_FIND_NEXT_DELEGATE;
        public int OP_GE;
        public int OP_GE_DECIMAL;
        public int OP_GE_DOUBLE;
        public int OP_GE_FLOAT;
        public int OP_GE_INT;
        public int OP_GE_LONG;
        public int OP_GE_STRING;
        public int OP_GE_UINT;
        public int OP_GE_ULONG;
        public int OP_GET_PARAM_VALUE;
        public int OP_GO;
        public int OP_GO_FALSE;
        public int OP_GO_NULL;
        public int OP_GO_TRUE;
        public int OP_GOTO_CONTINUE;
        public int OP_GOTO_START;
        public int OP_GT;
        public int OP_GT_DECIMAL;
        public int OP_GT_DOUBLE;
        public int OP_GT_FLOAT;
        public int OP_GT_INT;
        public int OP_GT_LONG;
        public int OP_GT_STRING;
        public int OP_GT_UINT;
        public int OP_GT_ULONG;
        public int OP_HALT;
        public int OP_INC;
        public int OP_INC_BYTE;
        public int OP_INC_CHAR;
        public int OP_INC_DECIMAL;
        public int OP_INC_DOUBLE;
        public int OP_INC_FLOAT;
        public int OP_INC_INT;
        public int OP_INC_LONG;
        public int OP_INC_SBYTE;
        public int OP_INC_SHORT;
        public int OP_INC_UINT;
        public int OP_INC_ULONG;
        public int OP_INC_USHORT;
        public int OP_INIT_METHOD;
        public int OP_INIT_STATIC_VAR;
        public int OP_INSERT_STRUCT_CONSTRUCTORS;
        public int OP_IS;
        public int OP_LABEL;
        public int OP_LE;
        public int OP_LE_DECIMAL;
        public int OP_LE_DOUBLE;
        public int OP_LE_FLOAT;
        public int OP_LE_INT;
        public int OP_LE_LONG;
        public int OP_LE_STRING;
        public int OP_LE_UINT;
        public int OP_LE_ULONG;
        public int OP_LEFT_SHIFT;
        public int OP_LEFT_SHIFT_INT;
        public int OP_LEFT_SHIFT_LONG;
        public int OP_LEFT_SHIFT_UINT;
        public int OP_LEFT_SHIFT_ULONG;
        public int OP_LOCK;
        public int OP_LOGICAL_AND;
        public int OP_LOGICAL_NEGATION_BOOL;
        public int OP_LOGICAL_OR;
        public int OP_LT;
        public int OP_LT_DECIMAL;
        public int OP_LT_DOUBLE;
        public int OP_LT_FLOAT;
        public int OP_LT_INT;
        public int OP_LT_LONG;
        public int OP_LT_STRING;
        public int OP_LT_UINT;
        public int OP_LT_ULONG;
        public int OP_MINUS;
        public int OP_MOD;
        public int OP_MULT;
        public int OP_MULTIPLICATION_DECIMAL;
        public int OP_MULTIPLICATION_DOUBLE;
        public int OP_MULTIPLICATION_FLOAT;
        public int OP_MULTIPLICATION_INT;
        public int OP_MULTIPLICATION_LONG;
        public int OP_MULTIPLICATION_UINT;
        public int OP_MULTIPLICATION_ULONG;
        public int OP_NE;
        public int OP_NE_BOOL;
        public int OP_NE_DECIMAL;
        public int OP_NE_DELEGATES;
        public int OP_NE_DOUBLE;
        public int OP_NE_FLOAT;
        public int OP_NE_INT;
        public int OP_NE_LONG;
        public int OP_NE_OBJECT;
        public int OP_NE_STRING;
        public int OP_NE_UINT;
        public int OP_NE_ULONG;
        public int OP_NEGATION_DECIMAL;
        public int OP_NEGATION_DOUBLE;
        public int OP_NEGATION_FLOAT;
        public int OP_NEGATION_INT;
        public int OP_NEGATION_LONG;
        public int OP_NOP;
        public int OP_NOT;
        public int OP_ONERROR;
        public int OP_PLUS;
        public int OP_PRINT;
        public int OP_PUSH;
        public int OP_RAISE_EVENT;
        public int OP_REMAINDER_DECIMAL;
        public int OP_REMAINDER_DOUBLE;
        public int OP_REMAINDER_FLOAT;
        public int OP_REMAINDER_INT;
        public int OP_REMAINDER_LONG;
        public int OP_REMAINDER_UINT;
        public int OP_REMAINDER_ULONG;
        public int OP_RESTORE_CHECKED_STATE;
        public int OP_RESUME;
        public int OP_RESUME_NEXT;
        public int OP_RET;
        public int OP_RIGHT_SHIFT;
        public int OP_RIGHT_SHIFT_INT;
        public int OP_RIGHT_SHIFT_LONG;
        public int OP_RIGHT_SHIFT_UINT;
        public int OP_RIGHT_SHIFT_ULONG;
        public int OP_SEPARATOR;
        public int OP_SET_DEFAULT;
        public int OP_SET_REF_TYPE;
        public int OP_SETUP_DELEGATE;
        public int OP_SETUP_INDEX_OBJECT;
        public int OP_START_REGION;
        public int OP_STRICT_OFF;
        public int OP_STRICT_ON;
        public int OP_SUB_DELEGATES;
        public int OP_SUBTRACTION_DECIMAL;
        public int OP_SUBTRACTION_DOUBLE;
        public int OP_SUBTRACTION_FLOAT;
        public int OP_SUBTRACTION_INT;
        public int OP_SUBTRACTION_LONG;
        public int OP_SUBTRACTION_UINT;
        public int OP_SUBTRACTION_ULONG;
        public int OP_SWAPPED_ARGUMENTS;
        public int OP_THROW;
        public int OP_TO_BOOLEAN;
        public int OP_TO_BYTE;
        public int OP_TO_CHAR;
        public int OP_TO_CHAR_ARRAY;
        public int OP_TO_DECIMAL;
        public int OP_TO_DOUBLE;
        public int OP_TO_ENUM;
        public int OP_TO_FLOAT;
        public int OP_TO_INT;
        public int OP_TO_LONG;
        public int OP_TO_SBYTE;
        public int OP_TO_SHORT;
        public int OP_TO_STRING;
        public int OP_TO_UINT;
        public int OP_TO_ULONG;
        public int OP_TO_USHORT;
        public int OP_TRUE;
        public int OP_TRY_OFF;
        public int OP_TRY_ON;
        public int OP_TYPEOF;
        public int OP_UNARY_MINUS;
        public int OP_UNARY_PLUS;
        public int OP_UNDEF;
        public int OP_UNLOCK;
        public int OP_UPCASE_OFF;
        public int OP_UPCASE_ON;
        public StringList Operators;
        public Hashtable overloadable_binary_operators_str;
        public Hashtable overloadable_unary_operators_str;
        public bool Paused;
        public ArrayList prog;
        public ProgRec r;
        private IntegerStack resume_stack;
        private BaseScripter scripter;
        private ObjectStack stack;
        private IntegerStack state_stack;
        private SymbolTable symbol_table;
        public bool Terminated;
        private TryStack try_stack;

        public Code(BaseScripter scripter)
        {
            int num;
            this.goto_line = 0;
            this.Terminated = false;
            this.Paused = false;
            this.curr_stack_count = 0;
            this.debugging = true;
            this.scripter = scripter;
            this.symbol_table = scripter.symbol_table;
            this.Operators = new StringList(true);
            this.OP_DUMMY = -this.Operators.Add("DUMMY");
            this.OP_UPCASE_ON = -this.Operators.Add("UPCASE ON");
            this.OP_UPCASE_OFF = -this.Operators.Add("UPCASE OFF");
            this.OP_EXPLICIT_ON = -this.Operators.Add("EXPLICIT ON");
            this.OP_EXPLICIT_OFF = -this.Operators.Add("EXPLICIT OFF");
            this.OP_STRICT_ON = -this.Operators.Add("STRICT ON");
            this.OP_STRICT_OFF = -this.Operators.Add("STRICT OFF");
            this.OP_HALT = -this.Operators.Add("HALT");
            this.OP_PRINT = -this.Operators.Add("PRINT");
            this.OP_SEPARATOR = -this.Operators.Add("SEP");
            this.OP_DEFINE = -this.Operators.Add("DEFINE");
            this.OP_UNDEF = -this.Operators.Add("UNDEF");
            this.OP_START_REGION = -this.Operators.Add("START REGION");
            this.OP_END_REGION = -this.Operators.Add("END REGION");
            this.OP_BEGIN_MODULE = -this.Operators.Add("BEGIN MODULE");
            this.OP_END_MODULE = -this.Operators.Add("END MODULE");
            this.OP_ASSIGN_TYPE = -this.Operators.Add("ASSIGN TYPE");
            this.OP_ASSIGN_COND_TYPE = -this.Operators.Add("ASSIGN COND TYPE");
            this.OP_CREATE_REF_TYPE = -this.Operators.Add("CREATE REF TYPE");
            this.OP_SET_REF_TYPE = -this.Operators.Add("SET REF TYPE");
            this.OP_CREATE_NAMESPACE = -this.Operators.Add("CREATE NAMESPACE");
            this.OP_CREATE_CLASS = -this.Operators.Add("CREATE CLASS");
            this.OP_END_CLASS = -this.Operators.Add("END CLASS");
            this.OP_CREATE_FIELD = -this.Operators.Add("CREATE FIELD");
            this.OP_CREATE_PROPERTY = -this.Operators.Add("CREATE PROPERTY");
            this.OP_CREATE_EVENT = -this.Operators.Add("CREATE EVENT");
            this.OP_BEGIN_USING = -this.Operators.Add("BEGIN USING");
            this.OP_END_USING = -this.Operators.Add("END USING");
            this.OP_CREATE_USING_ALIAS = -this.Operators.Add("CREATE USING ALIAS");
            this.OP_CREATE_TYPE_REFERENCE = -this.Operators.Add("CREATE TYPE REFERENCE");
            this.OP_ADD_MODIFIER = -this.Operators.Add("ADD MODIFIER");
            this.OP_ADD_ANCESTOR = -this.Operators.Add("ADD ANCESTOR");
            this.OP_ADD_UNDERLYING_TYPE = -this.Operators.Add("ADD UNDERLYING TYPE");
            this.OP_ADD_READ_ACCESSOR = -this.Operators.Add("ADD READ ACCESSOR");
            this.OP_ADD_WRITE_ACCESSOR = -this.Operators.Add("ADD WRITE ACCESSOR");
            this.OP_SET_DEFAULT = -this.Operators.Add("SET DEFAULT");
            this.OP_ADD_ADD_ACCESSOR = -this.Operators.Add("ADD ADD ACCESSOR");
            this.OP_ADD_REMOVE_ACCESSOR = -this.Operators.Add("ADD REMOVE ACCESSOR");
            this.OP_ADD_PATTERN = -this.Operators.Add("ADD PATTERN");
            this.OP_ADD_IMPLEMENTS = -this.Operators.Add("ADD IMPLEMENTS");
            this.OP_ADD_HANDLES = -this.Operators.Add("ADD HANDLES");
            this.OP_BEGIN_CALL = -this.Operators.Add("BEGIN CALL");
            this.OP_EVAL = -this.Operators.Add("EVAL");
            this.OP_EVAL_TYPE = -this.Operators.Add("EVAL TYPE");
            this.OP_EVAL_BASE_TYPE = -this.Operators.Add("EVAL BASE TYPE");
            this.OP_ASSIGN_NAME = -this.Operators.Add("ASSIGN NAME");
            this.OP_ADD_MIN_VALUE = -this.Operators.Add("ADD MIN VALUE");
            this.OP_ADD_MAX_VALUE = -this.Operators.Add("ADD MAX VALUE");
            this.OP_ADD_ARRAY_RANGE = -this.Operators.Add("ADD ARRAY RANGE");
            this.OP_ADD_ARRAY_INDEX = -this.Operators.Add("ADD ARRAY INDEX");
            this.OP_CREATE_METHOD = -this.Operators.Add("CREATE METHOD");
            this.OP_INIT_METHOD = -this.Operators.Add("INIT METHOD");
            this.OP_END_METHOD = -this.Operators.Add("END METHOD");
            this.OP_ADD_PARAM = -this.Operators.Add("ADD PARAM");
            this.OP_ADD_PARAMS = -this.Operators.Add("ADD PARAMS");
            this.OP_ADD_DEFAULT_VALUE = -this.Operators.Add("ADD DEFAULT VALUE");
            this.OP_DECLARE_LOCAL_VARIABLE = -this.Operators.Add("DECLARE LOCAL VARIABLE");
            this.OP_DECLARE_LOCAL_VARIABLE_RUNTIME = -this.Operators.Add("DECLARE LOCAL VARIABLE RUNTIME");
            this.OP_DECLARE_LOCAL_SIMPLE = -this.Operators.Add("DECLARE LOCAL SIMPLE");
            this.OP_ADD_EXPLICIT_INTERFACE = -this.Operators.Add("ADD EXPLICIT INTERFACE");
            this.OP_ADD_EVENT_FIELD = -this.Operators.Add("ADD EVENT FIELD");
            this.OP_CALL_BASE = -this.Operators.Add("CALL BASE");
            this.OP_CALL_SIMPLE = -this.Operators.Add("CALL SIMPLE");
            this.OP_CHECK_STRUCT_CONSTRUCTOR = -this.Operators.Add("CHECK STRUCT CONSTRUCTOR");
            this.OP_INSERT_STRUCT_CONSTRUCTORS = -this.Operators.Add("INSERT STRUCT CONSTRUCTORS");
            this.OP_NOP = -this.Operators.Add("NOP");
            this.OP_INIT_STATIC_VAR = -this.Operators.Add("INIT STATIC VAR");
            this.OP_LABEL = -this.Operators.Add("LABEL");
            this.OP_ASSIGN = -this.Operators.Add("=");
            this.OP_ASSIGN_STRUCT = -this.Operators.Add("= (struct)");
            this.OP_BITWISE_AND = -this.Operators.Add("&");
            this.OP_BITWISE_XOR = -this.Operators.Add("^");
            this.OP_BITWISE_OR = -this.Operators.Add("|");
            this.OP_LOGICAL_OR = -this.Operators.Add("||");
            this.OP_LOGICAL_AND = -this.Operators.Add("&&");
            this.OP_PLUS = -this.Operators.Add("+");
            this.OP_INC = -this.Operators.Add("++");
            this.OP_MINUS = -this.Operators.Add("-");
            this.OP_DEC = -this.Operators.Add("--");
            this.OP_MULT = -this.Operators.Add("*");
            this.OP_EXPONENT = -this.Operators.Add("EXP");
            this.OP_MOD = -this.Operators.Add("%");
            this.OP_DIV = -this.Operators.Add("-");
            this.OP_EQ = -this.Operators.Add("==");
            this.OP_NE = -this.Operators.Add("<>");
            this.OP_GT = -this.Operators.Add(">");
            this.OP_LT = -this.Operators.Add("<");
            this.OP_GE = -this.Operators.Add(">=");
            this.OP_LE = -this.Operators.Add("<=");
            this.OP_IS = -this.Operators.Add("is");
            this.OP_AS = -this.Operators.Add("as");
            this.OP_LEFT_SHIFT = -this.Operators.Add("<<");
            this.OP_RIGHT_SHIFT = -this.Operators.Add(">>");
            this.OP_UNARY_PLUS = -this.Operators.Add("+ (unary)");
            this.OP_UNARY_MINUS = -this.Operators.Add("- (unary)");
            this.OP_NOT = -this.Operators.Add("not");
            this.OP_COMPLEMENT = -this.Operators.Add("~");
            this.OP_TRUE = -this.Operators.Add("true");
            this.OP_FALSE = -this.Operators.Add("false");
            this.OP_GO = -this.Operators.Add("GO");
            this.OP_GO_FALSE = -this.Operators.Add("GO FALSE");
            this.OP_GO_TRUE = -this.Operators.Add("GO TRUE");
            this.OP_GO_NULL = -this.Operators.Add("GO NULL");
            this.OP_GOTO_START = -this.Operators.Add("GOTO START");
            this.OP_GOTO_CONTINUE = -this.Operators.Add("GOTO CONTINUE");
            this.OP_TRY_ON = -this.Operators.Add("TRY ON");
            this.OP_TRY_OFF = -this.Operators.Add("TRY OFF");
            this.OP_THROW = -this.Operators.Add("THROW");
            this.OP_CATCH = -this.Operators.Add("CATCH");
            this.OP_FINALLY = -this.Operators.Add("FINALLY");
            this.OP_DISCARD_ERROR = -this.Operators.Add("DISCARD ERROR");
            this.OP_EXIT_ON_ERROR = -this.Operators.Add("EXIT ON ERROR");
            this.OP_ONERROR = -this.Operators.Add("ON ERROR");
            this.OP_RESUME = -this.Operators.Add("RESUME");
            this.OP_RESUME_NEXT = -this.Operators.Add("RESUME NEXT");
            this.OP_CREATE_ARRAY_INSTANCE = -this.Operators.Add("CREATE ARRAY INSTANCE");
            this.OP_CREATE_OBJECT = -this.Operators.Add("CREATE OBJECT");
            this.OP_CREATE_REFERENCE = -this.Operators.Add("CREATE REFERENCE");
            this.OP_SETUP_DELEGATE = -this.Operators.Add("SETUP DELEGATE");
            this.OP_ADD_DELEGATES = -this.Operators.Add("ADD DELEGATES");
            this.OP_SUB_DELEGATES = -this.Operators.Add("SUB DELEGATES");
            this.OP_EQ_DELEGATES = -this.Operators.Add("EQ DELEGATES");
            this.OP_NE_DELEGATES = -this.Operators.Add("NE DELEGATES");
            this.OP_ADDRESS_OF = -this.Operators.Add("ADDRESS OF");
            this.OP_CREATE_INDEX_OBJECT = -this.Operators.Add("CREATE INDEX OBJECT");
            this.OP_ADD_INDEX = -this.Operators.Add("ADD INDEX");
            this.OP_SETUP_INDEX_OBJECT = -this.Operators.Add("SETUP INDEX OBJECT");
            this.OP_PUSH = -this.Operators.Add("PUSH");
            this.OP_CALL = -this.Operators.Add("CALL");
            this.OP_CALL_VIRT = -this.Operators.Add("CALL VIRT");
            this.OP_DYNAMIC_INVOKE = -this.Operators.Add("DYNAMIC INVOKE");
            this.OP_CALL_ADD_EVENT = -this.Operators.Add("CALL ADD EVENT");
            this.OP_RAISE_EVENT = -this.Operators.Add("RAISE EVENT");
            this.OP_FIND_FIRST_DELEGATE = -this.Operators.Add("FIND FIRST DELEGATE");
            this.OP_FIND_NEXT_DELEGATE = -this.Operators.Add("FIND NEXT DELEGATE");
            this.OP_RET = -this.Operators.Add("RET");
            this.OP_EXIT_SUB = -this.Operators.Add("EXIT SUB");
            this.OP_GET_PARAM_VALUE = -this.Operators.Add("GET PARAM VALUE");
            this.OP_CHECKED = -this.Operators.Add("CHECKED");
            this.OP_RESTORE_CHECKED_STATE = -this.Operators.Add("RESTORE CHECKED STATE");
            this.OP_DISPOSE = -this.Operators.Add("DISPOSE");
            this.OP_TYPEOF = -this.Operators.Add("TYPEOF");
            this.OP_LOCK = -this.Operators.Add("LOCK");
            this.OP_UNLOCK = -this.Operators.Add("UNLOCK");
            this.OP_CAST = -this.Operators.Add("CAST");
            this.OP_TO_SBYTE = -this.Operators.Add("TO SBYTE");
            this.OP_TO_BYTE = -this.Operators.Add("TO BYTE");
            this.OP_TO_USHORT = -this.Operators.Add("TO USHORT");
            this.OP_TO_SHORT = -this.Operators.Add("TO SHORT");
            this.OP_TO_UINT = -this.Operators.Add("TO UINT");
            this.OP_TO_INT = -this.Operators.Add("TO INT");
            this.OP_TO_ULONG = -this.Operators.Add("TO ULONG");
            this.OP_TO_LONG = -this.Operators.Add("TO LONG");
            this.OP_TO_CHAR = -this.Operators.Add("TO CHAR");
            this.OP_TO_FLOAT = -this.Operators.Add("TO FLOAT");
            this.OP_TO_DOUBLE = -this.Operators.Add("TO DOUBLE");
            this.OP_TO_DECIMAL = -this.Operators.Add("TO DECIMAL");
            this.OP_TO_STRING = -this.Operators.Add("TO STRING");
            this.OP_TO_BOOLEAN = -this.Operators.Add("TO BOOLEAN");
            this.OP_TO_ENUM = -this.Operators.Add("TO ENUM");
            this.OP_TO_CHAR_ARRAY = -this.Operators.Add("TO CHAR[]");
            this.OP_NEGATION_INT = -this.Operators.Add("-(unary int)");
            this.OP_NEGATION_LONG = -this.Operators.Add("-(unary long)");
            this.OP_NEGATION_FLOAT = -this.Operators.Add("-(unary float)");
            this.OP_NEGATION_DOUBLE = -this.Operators.Add("-(unary double)");
            this.OP_NEGATION_DECIMAL = -this.Operators.Add("-(unary decimal)");
            this.OP_LOGICAL_NEGATION_BOOL = -this.Operators.Add("!(bool)");
            this.OP_BITWISE_COMPLEMENT_INT = -this.Operators.Add("~(int)");
            this.OP_BITWISE_COMPLEMENT_UINT = -this.Operators.Add("~(uint)");
            this.OP_BITWISE_COMPLEMENT_LONG = -this.Operators.Add("~(long)");
            this.OP_BITWISE_COMPLEMENT_ULONG = -this.Operators.Add("~(ulong)");
            this.OP_INC_SBYTE = -this.Operators.Add("++(sbyte)");
            this.OP_INC_BYTE = -this.Operators.Add("++(byte)");
            this.OP_INC_SHORT = -this.Operators.Add("++(short)");
            this.OP_INC_USHORT = -this.Operators.Add("++(ushort)");
            this.OP_INC_INT = -this.Operators.Add("++(int)");
            this.OP_INC_UINT = -this.Operators.Add("++(uint)");
            this.OP_INC_LONG = -this.Operators.Add("++(long)");
            this.OP_INC_ULONG = -this.Operators.Add("++(ulong)");
            this.OP_INC_CHAR = -this.Operators.Add("++(char)");
            this.OP_INC_FLOAT = -this.Operators.Add("++(float)");
            this.OP_INC_DOUBLE = -this.Operators.Add("++(double)");
            this.OP_INC_DECIMAL = -this.Operators.Add("++(decimal)");
            this.OP_DEC_SBYTE = -this.Operators.Add("--(sbyte)");
            this.OP_DEC_BYTE = -this.Operators.Add("--(byte)");
            this.OP_DEC_SHORT = -this.Operators.Add("--(short)");
            this.OP_DEC_USHORT = -this.Operators.Add("--(ushort)");
            this.OP_DEC_INT = -this.Operators.Add("--(int)");
            this.OP_DEC_UINT = -this.Operators.Add("--(uint)");
            this.OP_DEC_LONG = -this.Operators.Add("--(long)");
            this.OP_DEC_ULONG = -this.Operators.Add("--(ulong)");
            this.OP_DEC_CHAR = -this.Operators.Add("--(char)");
            this.OP_DEC_FLOAT = -this.Operators.Add("--(float)");
            this.OP_DEC_DOUBLE = -this.Operators.Add("--(double)");
            this.OP_DEC_DECIMAL = -this.Operators.Add("--(decimal)");
            this.OP_ADDITION_INT = -this.Operators.Add("+(int)");
            this.OP_ADDITION_UINT = -this.Operators.Add("+(uint)");
            this.OP_ADDITION_LONG = -this.Operators.Add("+(long)");
            this.OP_ADDITION_ULONG = -this.Operators.Add("+(ulong)");
            this.OP_ADDITION_FLOAT = -this.Operators.Add("+(float)");
            this.OP_ADDITION_DOUBLE = -this.Operators.Add("+(double)");
            this.OP_ADDITION_DECIMAL = -this.Operators.Add("+(decimal)");
            this.OP_ADDITION_STRING = -this.Operators.Add("+(string)");
            this.OP_SUBTRACTION_INT = -this.Operators.Add("-(int)");
            this.OP_SUBTRACTION_UINT = -this.Operators.Add("-(uint)");
            this.OP_SUBTRACTION_LONG = -this.Operators.Add("-(long)");
            this.OP_SUBTRACTION_ULONG = -this.Operators.Add("-(ulong)");
            this.OP_SUBTRACTION_FLOAT = -this.Operators.Add("-(float)");
            this.OP_SUBTRACTION_DOUBLE = -this.Operators.Add("-(double)");
            this.OP_SUBTRACTION_DECIMAL = -this.Operators.Add("-(decimal)");
            this.OP_MULTIPLICATION_INT = -this.Operators.Add("*(int)");
            this.OP_MULTIPLICATION_UINT = -this.Operators.Add("*(uint)");
            this.OP_MULTIPLICATION_LONG = -this.Operators.Add("*(long)");
            this.OP_MULTIPLICATION_ULONG = -this.Operators.Add("*(ulong)");
            this.OP_MULTIPLICATION_FLOAT = -this.Operators.Add("*(float)");
            this.OP_MULTIPLICATION_DOUBLE = -this.Operators.Add("*(double)");
            this.OP_MULTIPLICATION_DECIMAL = -this.Operators.Add("*(decimal)");
            this.OP_EXPONENT_INT = -this.Operators.Add("EXPONENT(int)");
            this.OP_EXPONENT_UINT = -this.Operators.Add("EXPONENT(uint)");
            this.OP_EXPONENT_LONG = -this.Operators.Add("EXPONENT(long)");
            this.OP_EXPONENT_ULONG = -this.Operators.Add("EXPONENT(ulong)");
            this.OP_EXPONENT_FLOAT = -this.Operators.Add("EXPONENT(float)");
            this.OP_EXPONENT_DOUBLE = -this.Operators.Add("EXPONENT(double)");
            this.OP_EXPONENT_DECIMAL = -this.Operators.Add("EXPONENT(decimal)");
            this.OP_DIVISION_INT = -this.Operators.Add("/(int)");
            this.OP_DIVISION_UINT = -this.Operators.Add("/(uint)");
            this.OP_DIVISION_LONG = -this.Operators.Add("/(long)");
            this.OP_DIVISION_ULONG = -this.Operators.Add("/(ulong)");
            this.OP_DIVISION_FLOAT = -this.Operators.Add("/(float)");
            this.OP_DIVISION_DOUBLE = -this.Operators.Add("/(double)");
            this.OP_DIVISION_DECIMAL = -this.Operators.Add("/(decimal)");
            this.OP_REMAINDER_INT = -this.Operators.Add("%(int)");
            this.OP_REMAINDER_UINT = -this.Operators.Add("%(uint)");
            this.OP_REMAINDER_LONG = -this.Operators.Add("%(long)");
            this.OP_REMAINDER_ULONG = -this.Operators.Add("%(ulong)");
            this.OP_REMAINDER_FLOAT = -this.Operators.Add("%(float)");
            this.OP_REMAINDER_DOUBLE = -this.Operators.Add("%(double)");
            this.OP_REMAINDER_DECIMAL = -this.Operators.Add("%(decimal)");
            this.OP_LEFT_SHIFT_INT = -this.Operators.Add("<<(int)");
            this.OP_LEFT_SHIFT_UINT = -this.Operators.Add("<<(uint)");
            this.OP_LEFT_SHIFT_LONG = -this.Operators.Add("<<(long)");
            this.OP_LEFT_SHIFT_ULONG = -this.Operators.Add("<<(ulong)");
            this.OP_RIGHT_SHIFT_INT = -this.Operators.Add(">>(int)");
            this.OP_RIGHT_SHIFT_UINT = -this.Operators.Add(">>(uint)");
            this.OP_RIGHT_SHIFT_LONG = -this.Operators.Add(">>(long)");
            this.OP_RIGHT_SHIFT_ULONG = -this.Operators.Add(">>(ulong)");
            this.OP_BITWISE_AND_INT = -this.Operators.Add("&(int)");
            this.OP_BITWISE_AND_UINT = -this.Operators.Add("&(uint)");
            this.OP_BITWISE_AND_LONG = -this.Operators.Add("&(long)");
            this.OP_BITWISE_AND_ULONG = -this.Operators.Add("&(ulong)");
            this.OP_BITWISE_AND_BOOL = -this.Operators.Add("&(bool)");
            this.OP_BITWISE_OR_INT = -this.Operators.Add("|(int)");
            this.OP_BITWISE_OR_UINT = -this.Operators.Add("|(uint)");
            this.OP_BITWISE_OR_LONG = -this.Operators.Add("|(long)");
            this.OP_BITWISE_OR_ULONG = -this.Operators.Add("|(ulong)");
            this.OP_BITWISE_OR_BOOL = -this.Operators.Add("|(bool)");
            this.OP_BITWISE_XOR_INT = -this.Operators.Add("^(int)");
            this.OP_BITWISE_XOR_UINT = -this.Operators.Add("^(uint)");
            this.OP_BITWISE_XOR_LONG = -this.Operators.Add("^(long)");
            this.OP_BITWISE_XOR_ULONG = -this.Operators.Add("^(ulong)");
            this.OP_BITWISE_XOR_BOOL = -this.Operators.Add("^(bool)");
            this.OP_LT_INT = -this.Operators.Add("<(int)");
            this.OP_LT_UINT = -this.Operators.Add("<(uint)");
            this.OP_LT_LONG = -this.Operators.Add("<(long)");
            this.OP_LT_ULONG = -this.Operators.Add("<(ulong)");
            this.OP_LT_FLOAT = -this.Operators.Add("<(float)");
            this.OP_LT_DOUBLE = -this.Operators.Add("<(double)");
            this.OP_LT_DECIMAL = -this.Operators.Add("<(decimal)");
            this.OP_LT_STRING = -this.Operators.Add("<(string)");
            this.OP_LE_INT = -this.Operators.Add("<=(int)");
            this.OP_LE_UINT = -this.Operators.Add("<=(uint)");
            this.OP_LE_LONG = -this.Operators.Add("<=(long)");
            this.OP_LE_ULONG = -this.Operators.Add("<=(ulong)");
            this.OP_LE_FLOAT = -this.Operators.Add("<=(float)");
            this.OP_LE_DOUBLE = -this.Operators.Add("<=(double)");
            this.OP_LE_DECIMAL = -this.Operators.Add("<=(decimal)");
            this.OP_LE_STRING = -this.Operators.Add("<=(string)");
            this.OP_GT_INT = -this.Operators.Add(">(int)");
            this.OP_GT_UINT = -this.Operators.Add(">(uint)");
            this.OP_GT_LONG = -this.Operators.Add(">(long)");
            this.OP_GT_ULONG = -this.Operators.Add(">(ulong)");
            this.OP_GT_FLOAT = -this.Operators.Add(">(float)");
            this.OP_GT_DOUBLE = -this.Operators.Add(">(double)");
            this.OP_GT_DECIMAL = -this.Operators.Add(">(decimal)");
            this.OP_GT_STRING = -this.Operators.Add(">(string)");
            this.OP_GE_INT = -this.Operators.Add(">=(int)");
            this.OP_GE_UINT = -this.Operators.Add(">=(uint)");
            this.OP_GE_LONG = -this.Operators.Add(">=(long)");
            this.OP_GE_ULONG = -this.Operators.Add(">=(ulong)");
            this.OP_GE_FLOAT = -this.Operators.Add(">=(float)");
            this.OP_GE_DOUBLE = -this.Operators.Add(">=(double)");
            this.OP_GE_DECIMAL = -this.Operators.Add(">=(decimal)");
            this.OP_GE_STRING = -this.Operators.Add(">=(string)");
            this.OP_EQ_INT = -this.Operators.Add("==(int)");
            this.OP_EQ_UINT = -this.Operators.Add("==(uint)");
            this.OP_EQ_LONG = -this.Operators.Add("==(long)");
            this.OP_EQ_ULONG = -this.Operators.Add("==(ulong)");
            this.OP_EQ_FLOAT = -this.Operators.Add("==(float)");
            this.OP_EQ_DOUBLE = -this.Operators.Add("==(double)");
            this.OP_EQ_DECIMAL = -this.Operators.Add("==(decimal)");
            this.OP_EQ_STRING = -this.Operators.Add("==(string)");
            this.OP_EQ_BOOL = -this.Operators.Add("==(bool)");
            this.OP_EQ_OBJECT = -this.Operators.Add("==(object)");
            this.OP_NE_INT = -this.Operators.Add("!=(int)");
            this.OP_NE_UINT = -this.Operators.Add("!=(uint)");
            this.OP_NE_LONG = -this.Operators.Add("!=(long)");
            this.OP_NE_ULONG = -this.Operators.Add("!=(ulong)");
            this.OP_NE_FLOAT = -this.Operators.Add("!=(float)");
            this.OP_NE_DOUBLE = -this.Operators.Add("!=(double)");
            this.OP_NE_DECIMAL = -this.Operators.Add("!=(decimal)");
            this.OP_NE_STRING = -this.Operators.Add("!=(string)");
            this.OP_NE_BOOL = -this.Operators.Add("!=(bool)");
            this.OP_NE_OBJECT = -this.Operators.Add("!=(object)");
            this.OP_SWAPPED_ARGUMENTS = -this.Operators.Add("SWAPPED ARG");
            this.overloadable_unary_operators_str = new Hashtable();
            this.overloadable_unary_operators_str.Add(this.OP_UNARY_PLUS, "op_UnaryPlus");
            this.overloadable_unary_operators_str.Add(this.OP_UNARY_MINUS, "op_UnaryNegation");
            this.overloadable_unary_operators_str.Add(this.OP_NOT, "op_LogicalNot");
            this.overloadable_unary_operators_str.Add(this.OP_COMPLEMENT, "op_OnesComplement");
            this.overloadable_unary_operators_str.Add(this.OP_INC, "op_Increment");
            this.overloadable_unary_operators_str.Add(this.OP_DEC, "op_Decrement");
            this.overloadable_unary_operators_str.Add(this.OP_TRUE, "op_True");
            this.overloadable_unary_operators_str.Add(this.OP_FALSE, "op_False");
            this.overloadable_binary_operators_str = new Hashtable();
            this.overloadable_binary_operators_str.Add(this.OP_PLUS, "op_Addition");
            this.overloadable_binary_operators_str.Add(this.OP_MINUS, "op_Subtraction");
            this.overloadable_binary_operators_str.Add(this.OP_MULT, "op_Multiply");
            this.overloadable_binary_operators_str.Add(this.OP_DIV, "op_Division");
            this.overloadable_binary_operators_str.Add(this.OP_MOD, "op_Modulus");
            this.overloadable_binary_operators_str.Add(this.OP_BITWISE_AND, "op_BitwiseAnd");
            this.overloadable_binary_operators_str.Add(this.OP_BITWISE_OR, "op_BitwiseOr");
            this.overloadable_binary_operators_str.Add(this.OP_BITWISE_XOR, "op_ExclusiveOr");
            this.overloadable_binary_operators_str.Add(this.OP_LEFT_SHIFT, "op_LeftShift");
            this.overloadable_binary_operators_str.Add(this.OP_RIGHT_SHIFT, "op_RightShift");
            this.overloadable_binary_operators_str.Add(this.OP_EQ, "op_Equality");
            this.overloadable_binary_operators_str.Add(this.OP_NE, "op_Inequality");
            this.overloadable_binary_operators_str.Add(this.OP_GT, "op_GreaterThan");
            this.overloadable_binary_operators_str.Add(this.OP_LT, "op_LessThan");
            this.overloadable_binary_operators_str.Add(this.OP_GE, "op_GreaterThanOrEqual");
            this.overloadable_binary_operators_str.Add(this.OP_LE, "op_LessThanOrEqual");
            this.detailed_operators = new Contact_Integers(200);
            this.detailed_negation_operators = new Contact_Integers(5);
            this.detailed_negation_operators.Add(8, this.OP_NEGATION_INT);
            this.detailed_negation_operators.Add(9, this.OP_NEGATION_LONG);
            this.detailed_negation_operators.Add(7, this.OP_NEGATION_FLOAT);
            this.detailed_negation_operators.Add(6, this.OP_NEGATION_DOUBLE);
            this.detailed_negation_operators.Add(5, this.OP_NEGATION_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_negation_operators);
            this.detailed_logical_negation_operators = new Contact_Integers(1);
            this.detailed_logical_negation_operators.Add(2, this.OP_LOGICAL_NEGATION_BOOL);
            this.detailed_operators.AddFrom(this.detailed_logical_negation_operators);
            this.detailed_bitwise_complement_operators = new Contact_Integers(4);
            this.detailed_bitwise_complement_operators.Add(8, this.OP_BITWISE_COMPLEMENT_INT);
            this.detailed_bitwise_complement_operators.Add(13, this.OP_BITWISE_COMPLEMENT_UINT);
            this.detailed_bitwise_complement_operators.Add(9, this.OP_BITWISE_COMPLEMENT_LONG);
            this.detailed_bitwise_complement_operators.Add(14, this.OP_BITWISE_COMPLEMENT_ULONG);
            this.detailed_operators.AddFrom(this.detailed_bitwise_complement_operators);
            this.detailed_inc_operators = new Contact_Integers(12);
            this.detailed_inc_operators.Add(10, this.OP_INC_SBYTE);
            this.detailed_inc_operators.Add(3, this.OP_INC_BYTE);
            this.detailed_inc_operators.Add(11, this.OP_INC_SHORT);
            this.detailed_inc_operators.Add(15, this.OP_INC_USHORT);
            this.detailed_inc_operators.Add(8, this.OP_INC_INT);
            this.detailed_inc_operators.Add(13, this.OP_INC_UINT);
            this.detailed_inc_operators.Add(9, this.OP_INC_LONG);
            this.detailed_inc_operators.Add(14, this.OP_INC_ULONG);
            this.detailed_inc_operators.Add(4, this.OP_INC_CHAR);
            this.detailed_inc_operators.Add(7, this.OP_INC_FLOAT);
            this.detailed_inc_operators.Add(6, this.OP_INC_DOUBLE);
            this.detailed_inc_operators.Add(5, this.OP_INC_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_inc_operators);
            this.detailed_dec_operators = new Contact_Integers(12);
            this.detailed_dec_operators.Add(10, this.OP_DEC_SBYTE);
            this.detailed_dec_operators.Add(3, this.OP_DEC_BYTE);
            this.detailed_dec_operators.Add(11, this.OP_DEC_SHORT);
            this.detailed_dec_operators.Add(15, this.OP_DEC_USHORT);
            this.detailed_dec_operators.Add(8, this.OP_DEC_INT);
            this.detailed_dec_operators.Add(13, this.OP_DEC_UINT);
            this.detailed_dec_operators.Add(9, this.OP_DEC_LONG);
            this.detailed_dec_operators.Add(14, this.OP_DEC_ULONG);
            this.detailed_dec_operators.Add(4, this.OP_DEC_CHAR);
            this.detailed_dec_operators.Add(7, this.OP_DEC_FLOAT);
            this.detailed_dec_operators.Add(6, this.OP_DEC_DOUBLE);
            this.detailed_dec_operators.Add(5, this.OP_DEC_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_dec_operators);
            this.detailed_addition_operators = new Contact_Integers(8);
            this.detailed_addition_operators.Add(8, this.OP_ADDITION_INT);
            this.detailed_addition_operators.Add(13, this.OP_ADDITION_UINT);
            this.detailed_addition_operators.Add(9, this.OP_ADDITION_LONG);
            this.detailed_addition_operators.Add(14, this.OP_ADDITION_ULONG);
            this.detailed_addition_operators.Add(7, this.OP_ADDITION_FLOAT);
            this.detailed_addition_operators.Add(6, this.OP_ADDITION_DOUBLE);
            this.detailed_addition_operators.Add(5, this.OP_ADDITION_DECIMAL);
            this.detailed_addition_operators.Add(12, this.OP_ADDITION_STRING);
            this.detailed_operators.AddFrom(this.detailed_addition_operators);
            this.detailed_subtraction_operators = new Contact_Integers(7);
            this.detailed_subtraction_operators.Add(8, this.OP_SUBTRACTION_INT);
            this.detailed_subtraction_operators.Add(13, this.OP_SUBTRACTION_UINT);
            this.detailed_subtraction_operators.Add(9, this.OP_SUBTRACTION_LONG);
            this.detailed_subtraction_operators.Add(14, this.OP_SUBTRACTION_ULONG);
            this.detailed_subtraction_operators.Add(7, this.OP_SUBTRACTION_FLOAT);
            this.detailed_subtraction_operators.Add(6, this.OP_SUBTRACTION_DOUBLE);
            this.detailed_subtraction_operators.Add(5, this.OP_SUBTRACTION_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_subtraction_operators);
            this.detailed_multiplication_operators = new Contact_Integers(7);
            this.detailed_multiplication_operators.Add(8, this.OP_MULTIPLICATION_INT);
            this.detailed_multiplication_operators.Add(13, this.OP_MULTIPLICATION_UINT);
            this.detailed_multiplication_operators.Add(9, this.OP_MULTIPLICATION_LONG);
            this.detailed_multiplication_operators.Add(14, this.OP_MULTIPLICATION_ULONG);
            this.detailed_multiplication_operators.Add(7, this.OP_MULTIPLICATION_FLOAT);
            this.detailed_multiplication_operators.Add(6, this.OP_MULTIPLICATION_DOUBLE);
            this.detailed_multiplication_operators.Add(5, this.OP_MULTIPLICATION_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_multiplication_operators);
            this.detailed_exponent_operators = new Contact_Integers(7);
            this.detailed_exponent_operators.Add(8, this.OP_EXPONENT_INT);
            this.detailed_exponent_operators.Add(13, this.OP_EXPONENT_UINT);
            this.detailed_exponent_operators.Add(9, this.OP_EXPONENT_LONG);
            this.detailed_exponent_operators.Add(14, this.OP_EXPONENT_ULONG);
            this.detailed_exponent_operators.Add(7, this.OP_EXPONENT_FLOAT);
            this.detailed_exponent_operators.Add(6, this.OP_EXPONENT_DOUBLE);
            this.detailed_exponent_operators.Add(5, this.OP_EXPONENT_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_exponent_operators);
            this.detailed_division_operators = new Contact_Integers(7);
            this.detailed_division_operators.Add(8, this.OP_DIVISION_INT);
            this.detailed_division_operators.Add(13, this.OP_DIVISION_UINT);
            this.detailed_division_operators.Add(9, this.OP_DIVISION_LONG);
            this.detailed_division_operators.Add(14, this.OP_DIVISION_ULONG);
            this.detailed_division_operators.Add(7, this.OP_DIVISION_FLOAT);
            this.detailed_division_operators.Add(6, this.OP_DIVISION_DOUBLE);
            this.detailed_division_operators.Add(5, this.OP_DIVISION_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_division_operators);
            this.detailed_remainder_operators = new Contact_Integers(7);
            this.detailed_remainder_operators.Add(8, this.OP_REMAINDER_INT);
            this.detailed_remainder_operators.Add(13, this.OP_REMAINDER_UINT);
            this.detailed_remainder_operators.Add(9, this.OP_REMAINDER_LONG);
            this.detailed_remainder_operators.Add(14, this.OP_REMAINDER_ULONG);
            this.detailed_remainder_operators.Add(7, this.OP_REMAINDER_FLOAT);
            this.detailed_remainder_operators.Add(6, this.OP_REMAINDER_DOUBLE);
            this.detailed_remainder_operators.Add(5, this.OP_REMAINDER_DECIMAL);
            this.detailed_operators.AddFrom(this.detailed_remainder_operators);
            this.detailed_left_shift_operators = new Contact_Integers(4);
            this.detailed_left_shift_operators.Add(8, this.OP_LEFT_SHIFT_INT);
            this.detailed_left_shift_operators.Add(13, this.OP_LEFT_SHIFT_UINT);
            this.detailed_left_shift_operators.Add(9, this.OP_LEFT_SHIFT_LONG);
            this.detailed_left_shift_operators.Add(14, this.OP_LEFT_SHIFT_ULONG);
            this.detailed_operators.AddFrom(this.detailed_left_shift_operators);
            this.detailed_right_shift_operators = new Contact_Integers(4);
            this.detailed_right_shift_operators.Add(8, this.OP_RIGHT_SHIFT_INT);
            this.detailed_right_shift_operators.Add(13, this.OP_RIGHT_SHIFT_UINT);
            this.detailed_right_shift_operators.Add(9, this.OP_RIGHT_SHIFT_LONG);
            this.detailed_right_shift_operators.Add(14, this.OP_RIGHT_SHIFT_ULONG);
            this.detailed_operators.AddFrom(this.detailed_right_shift_operators);
            this.detailed_bitwise_and_operators = new Contact_Integers(5);
            this.detailed_bitwise_and_operators.Add(8, this.OP_BITWISE_AND_INT);
            this.detailed_bitwise_and_operators.Add(13, this.OP_BITWISE_AND_UINT);
            this.detailed_bitwise_and_operators.Add(9, this.OP_BITWISE_AND_LONG);
            this.detailed_bitwise_and_operators.Add(14, this.OP_BITWISE_AND_ULONG);
            this.detailed_bitwise_and_operators.Add(2, this.OP_BITWISE_AND_BOOL);
            this.detailed_operators.AddFrom(this.detailed_bitwise_and_operators);
            this.detailed_bitwise_or_operators = new Contact_Integers(5);
            this.detailed_bitwise_or_operators.Add(8, this.OP_BITWISE_OR_INT);
            this.detailed_bitwise_or_operators.Add(13, this.OP_BITWISE_OR_UINT);
            this.detailed_bitwise_or_operators.Add(9, this.OP_BITWISE_OR_LONG);
            this.detailed_bitwise_or_operators.Add(14, this.OP_BITWISE_OR_ULONG);
            this.detailed_bitwise_or_operators.Add(2, this.OP_BITWISE_OR_BOOL);
            this.detailed_operators.AddFrom(this.detailed_bitwise_or_operators);
            this.detailed_bitwise_xor_operators = new Contact_Integers(5);
            this.detailed_bitwise_xor_operators.Add(8, this.OP_BITWISE_XOR_INT);
            this.detailed_bitwise_xor_operators.Add(13, this.OP_BITWISE_XOR_UINT);
            this.detailed_bitwise_xor_operators.Add(9, this.OP_BITWISE_XOR_LONG);
            this.detailed_bitwise_xor_operators.Add(14, this.OP_BITWISE_XOR_ULONG);
            this.detailed_bitwise_xor_operators.Add(2, this.OP_BITWISE_XOR_BOOL);
            this.detailed_operators.AddFrom(this.detailed_bitwise_xor_operators);
            this.detailed_lt_operators = new Contact_Integers(8);
            this.detailed_lt_operators.Add(8, this.OP_LT_INT);
            this.detailed_lt_operators.Add(13, this.OP_LT_UINT);
            this.detailed_lt_operators.Add(9, this.OP_LT_LONG);
            this.detailed_lt_operators.Add(14, this.OP_LT_ULONG);
            this.detailed_lt_operators.Add(7, this.OP_LT_FLOAT);
            this.detailed_lt_operators.Add(6, this.OP_LT_DOUBLE);
            this.detailed_lt_operators.Add(5, this.OP_LT_DECIMAL);
            this.detailed_lt_operators.Add(12, this.OP_LT_STRING);
            this.detailed_operators.AddFrom(this.detailed_lt_operators);
            this.detailed_le_operators = new Contact_Integers(8);
            this.detailed_le_operators.Add(8, this.OP_LE_INT);
            this.detailed_le_operators.Add(13, this.OP_LE_UINT);
            this.detailed_le_operators.Add(9, this.OP_LE_LONG);
            this.detailed_le_operators.Add(14, this.OP_LE_ULONG);
            this.detailed_le_operators.Add(7, this.OP_LE_FLOAT);
            this.detailed_le_operators.Add(6, this.OP_LE_DOUBLE);
            this.detailed_le_operators.Add(5, this.OP_LE_DECIMAL);
            this.detailed_le_operators.Add(12, this.OP_LE_STRING);
            this.detailed_operators.AddFrom(this.detailed_le_operators);
            this.detailed_gt_operators = new Contact_Integers(8);
            this.detailed_gt_operators.Add(8, this.OP_GT_INT);
            this.detailed_gt_operators.Add(13, this.OP_GT_UINT);
            this.detailed_gt_operators.Add(9, this.OP_GT_LONG);
            this.detailed_gt_operators.Add(14, this.OP_GT_ULONG);
            this.detailed_gt_operators.Add(7, this.OP_GT_FLOAT);
            this.detailed_gt_operators.Add(6, this.OP_GT_DOUBLE);
            this.detailed_gt_operators.Add(5, this.OP_GT_DECIMAL);
            this.detailed_gt_operators.Add(12, this.OP_GT_STRING);
            this.detailed_operators.AddFrom(this.detailed_gt_operators);
            this.detailed_ge_operators = new Contact_Integers(8);
            this.detailed_ge_operators.Add(8, this.OP_GE_INT);
            this.detailed_ge_operators.Add(13, this.OP_GE_UINT);
            this.detailed_ge_operators.Add(9, this.OP_GE_LONG);
            this.detailed_ge_operators.Add(14, this.OP_GE_ULONG);
            this.detailed_ge_operators.Add(7, this.OP_GE_FLOAT);
            this.detailed_ge_operators.Add(6, this.OP_GE_DOUBLE);
            this.detailed_ge_operators.Add(5, this.OP_GE_DECIMAL);
            this.detailed_ge_operators.Add(12, this.OP_GE_STRING);
            this.detailed_operators.AddFrom(this.detailed_ge_operators);
            this.detailed_eq_operators = new Contact_Integers(10);
            this.detailed_eq_operators.Add(8, this.OP_EQ_INT);
            this.detailed_eq_operators.Add(13, this.OP_EQ_UINT);
            this.detailed_eq_operators.Add(9, this.OP_EQ_LONG);
            this.detailed_eq_operators.Add(14, this.OP_EQ_ULONG);
            this.detailed_eq_operators.Add(7, this.OP_EQ_FLOAT);
            this.detailed_eq_operators.Add(6, this.OP_EQ_DOUBLE);
            this.detailed_eq_operators.Add(5, this.OP_EQ_DECIMAL);
            this.detailed_eq_operators.Add(12, this.OP_EQ_STRING);
            this.detailed_eq_operators.Add(2, this.OP_EQ_BOOL);
            this.detailed_eq_operators.Add(0x10, this.OP_EQ_OBJECT);
            this.detailed_operators.AddFrom(this.detailed_eq_operators);
            this.detailed_ne_operators = new Contact_Integers(10);
            this.detailed_ne_operators.Add(8, this.OP_NE_INT);
            this.detailed_ne_operators.Add(13, this.OP_NE_UINT);
            this.detailed_ne_operators.Add(9, this.OP_NE_LONG);
            this.detailed_ne_operators.Add(14, this.OP_NE_ULONG);
            this.detailed_ne_operators.Add(7, this.OP_NE_FLOAT);
            this.detailed_ne_operators.Add(6, this.OP_NE_DOUBLE);
            this.detailed_ne_operators.Add(5, this.OP_NE_DECIMAL);
            this.detailed_ne_operators.Add(12, this.OP_NE_STRING);
            this.detailed_ne_operators.Add(2, this.OP_NE_BOOL);
            this.detailed_ne_operators.Add(0x10, this.OP_NE_OBJECT);
            this.detailed_operators.AddFrom(this.detailed_ne_operators);
            this.stack = new ObjectStack();
            this.state_stack = new IntegerStack();
            this.try_stack = new TryStack();
            this.checked_stack = new ObjectStack();
            this.custom_ex_list = new CSLite_TypedList(false);
            this.breakpoint_list = new BreakpointList(scripter);
            this.callstack = new CallStack(scripter);
            this.resume_stack = new IntegerStack();
            this.get_item_list = new ArrayList();
            this.arrProc = new ArrayList();
            for (num = 0; num < 0x3e8; num++)
            {
                this.arrProc.Add(null);
            }
            this.arrProc[-this.OP_BEGIN_MODULE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_END_MODULE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CHECKED] = new Oper(this.OperChecked);
            this.arrProc[-this.OP_RESTORE_CHECKED_STATE] = new Oper(this.OperRestoreCheckedState);
            this.arrProc[-this.OP_LOCK] = new Oper(this.OperLock);
            this.arrProc[-this.OP_UNLOCK] = new Oper(this.OperUnlock);
            this.arrProc[-this.OP_DISPOSE] = new Oper(this.OperDispose);
            this.arrProc[-this.OP_TYPEOF] = new Oper(this.OperTypeOf);
            this.arrProc[-this.OP_CREATE_CLASS] = new Oper(this.OperCreateClass);
            this.arrProc[-this.OP_END_CLASS] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CREATE_OBJECT] = new Oper(this.OperCreateObject);
            this.arrProc[-this.OP_CREATE_REFERENCE] = new Oper(this.OperCreateReference);
            this.arrProc[-this.OP_CREATE_ARRAY_INSTANCE] = new Oper(this.OperCreateArrayInstance);
            this.arrProc[-this.OP_SETUP_DELEGATE] = new Oper(this.OperSetupDelegate);
            this.arrProc[-this.OP_ADD_DELEGATES] = new Oper(this.OperAddDelegates);
            this.arrProc[-this.OP_SUB_DELEGATES] = new Oper(this.OperSubDelegates);
            this.arrProc[-this.OP_EQ_DELEGATES] = new Oper(this.OperEqDelegates);
            this.arrProc[-this.OP_NE_DELEGATES] = new Oper(this.OperNeDelegates);
            this.arrProc[-this.OP_ADDRESS_OF] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CREATE_INDEX_OBJECT] = new Oper(this.OperCreateIndexObject);
            this.arrProc[-this.OP_ADD_INDEX] = new Oper(this.OperAddIndex);
            this.arrProc[-this.OP_SETUP_INDEX_OBJECT] = new Oper(this.OperSetupIndexObject);
            this.arrProc[-this.OP_CREATE_TYPE_REFERENCE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CREATE_FIELD] = new Oper(this.OperCreateField);
            this.arrProc[-this.OP_CREATE_PROPERTY] = new Oper(this.OperCreateProperty);
            this.arrProc[-this.OP_CREATE_EVENT] = new Oper(this.OperCreateEvent);
            this.arrProc[-this.OP_CREATE_NAMESPACE] = new Oper(this.OperCreateNamespace);
            this.arrProc[-this.OP_BEGIN_USING] = new Oper(this.OperNop);
            this.arrProc[-this.OP_END_USING] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_MODIFIER] = new Oper(this.OperAddModifier);
            this.arrProc[-this.OP_ADD_ANCESTOR] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_UNDERLYING_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_READ_ACCESSOR] = new Oper(this.OperAddReadAccessor);
            this.arrProc[-this.OP_ADD_WRITE_ACCESSOR] = new Oper(this.OperAddWriteAccessor);
            this.arrProc[-this.OP_SET_DEFAULT] = new Oper(this.OperSetDefault);
            this.arrProc[-this.OP_ADD_ADD_ACCESSOR] = new Oper(this.OperAddAddAccessor);
            this.arrProc[-this.OP_ADD_REMOVE_ACCESSOR] = new Oper(this.OperAddRemoveAccessor);
            this.arrProc[-this.OP_ADD_PATTERN] = new Oper(this.OperAddPattern);
            this.arrProc[-this.OP_ADD_IMPLEMENTS] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_HANDLES] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_MIN_VALUE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_MAX_VALUE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_ARRAY_RANGE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ADD_ARRAY_INDEX] = new Oper(this.OperNop);
            this.arrProc[-this.OP_BEGIN_CALL] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ASSIGN_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ASSIGN_COND_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CREATE_USING_ALIAS] = new Oper(this.OperCreateUsingAlias);
            this.arrProc[-this.OP_CREATE_REF_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_SET_REF_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_EVAL] = new Oper(this.OperNop);
            this.arrProc[-this.OP_EVAL_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_EVAL_BASE_TYPE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ASSIGN_NAME] = new Oper(this.OperNop);
            this.arrProc[-this.OP_NOP] = new Oper(this.OperNop);
            this.arrProc[-this.OP_INIT_STATIC_VAR] = new Oper(this.OperNop);
            this.arrProc[-this.OP_LABEL] = new Oper(this.OperNop);
            this.arrProc[-this.OP_ASSIGN] = new Oper(this.OperAssign);
            this.arrProc[-this.OP_ASSIGN_STRUCT] = new Oper(this.OperAssignStruct);
            this.arrProc[-this.OP_PLUS] = new Oper(this.OperPlus);
            this.arrProc[-this.OP_INC] = new Oper(this.OperPlus);
            this.arrProc[-this.OP_MINUS] = new Oper(this.OperMinus);
            this.arrProc[-this.OP_DEC] = new Oper(this.OperMinus);
            this.arrProc[-this.OP_MULT] = new Oper(this.OperMult);
            this.arrProc[-this.OP_EXPONENT] = new Oper(this.OperExp);
            this.arrProc[-this.OP_DIV] = new Oper(this.OperDiv);
            this.arrProc[-this.OP_EQ] = new Oper(this.OperEq);
            this.arrProc[-this.OP_NE] = new Oper(this.OperNe);
            this.arrProc[-this.OP_GT] = new Oper(this.OperGt);
            this.arrProc[-this.OP_GE] = new Oper(this.OperGe);
            this.arrProc[-this.OP_LT] = new Oper(this.OperLt);
            this.arrProc[-this.OP_LE] = new Oper(this.OperLe);
            this.arrProc[-this.OP_IS] = new Oper(this.OperIs);
            this.arrProc[-this.OP_AS] = new Oper(this.OperAs);
            this.arrProc[-this.OP_UPCASE_ON] = new Oper(this.OperNop);
            this.arrProc[-this.OP_UPCASE_OFF] = new Oper(this.OperNop);
            this.arrProc[-this.OP_EXPLICIT_ON] = new Oper(this.OperNop);
            this.arrProc[-this.OP_EXPLICIT_OFF] = new Oper(this.OperNop);
            this.arrProc[-this.OP_STRICT_ON] = new Oper(this.OperNop);
            this.arrProc[-this.OP_STRICT_OFF] = new Oper(this.OperNop);
            this.arrProc[-this.OP_HALT] = new Oper(this.OperHalt);
            this.arrProc[-this.OP_PRINT] = new Oper(this.OperPrint);
            this.arrProc[-this.OP_GO] = new Oper(this.OperGo);
            this.arrProc[-this.OP_GO_FALSE] = new Oper(this.OperGoFalse);
            this.arrProc[-this.OP_GO_TRUE] = new Oper(this.OperGoTrue);
            this.arrProc[-this.OP_GO_NULL] = new Oper(this.OperGoNull);
            this.arrProc[-this.OP_GOTO_START] = new Oper(this.OperGotoStart);
            this.arrProc[-this.OP_GOTO_CONTINUE] = new Oper(this.OperGotoContinue);
            this.arrProc[-this.OP_CREATE_METHOD] = new Oper(this.OperCreateMethod);
            this.arrProc[-this.OP_ADD_EXPLICIT_INTERFACE] = new Oper(this.OperAddExplicitInterface);
            this.arrProc[-this.OP_ADD_PARAM] = new Oper(this.OperAddParam);
            this.arrProc[-this.OP_ADD_PARAMS] = new Oper(this.OperAddParams);
            this.arrProc[-this.OP_ADD_DEFAULT_VALUE] = new Oper(this.OperAddDefaultValue);
            this.arrProc[-this.OP_INIT_METHOD] = new Oper(this.OperInitMethod);
            this.arrProc[-this.OP_DECLARE_LOCAL_VARIABLE] = new Oper(this.OperDeclareLocalVariable);
            this.arrProc[-this.OP_DECLARE_LOCAL_VARIABLE_RUNTIME] = new Oper(this.OperNop);
            this.arrProc[-this.OP_DECLARE_LOCAL_SIMPLE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_END_METHOD] = new Oper(this.OperEndMethod);
            this.arrProc[-this.OP_ADD_EVENT_FIELD] = new Oper(this.OperAddEventField);
            this.arrProc[-this.OP_CALL] = new Oper(this.OperCall);
            this.arrProc[-this.OP_CALL_BASE] = new Oper(this.OperCallBase);
            this.arrProc[-this.OP_CALL_SIMPLE] = new Oper(this.OperCall);
            this.arrProc[-this.OP_CALL_VIRT] = new Oper(this.OperCallVirt);
            this.arrProc[-this.OP_DYNAMIC_INVOKE] = new Oper(this.OperDynamicInvoke);
            this.arrProc[-this.OP_CHECK_STRUCT_CONSTRUCTOR] = new Oper(this.OperNop);
            this.arrProc[-this.OP_INSERT_STRUCT_CONSTRUCTORS] = new Oper(this.OperNop);
            this.arrProc[-this.OP_CALL_ADD_EVENT] = new Oper(this.OperCallAddEvent);
            this.arrProc[-this.OP_RAISE_EVENT] = new Oper(this.OperNop);
            this.arrProc[-this.OP_FIND_FIRST_DELEGATE] = new Oper(this.OperFindFirstDelegate);
            this.arrProc[-this.OP_FIND_NEXT_DELEGATE] = new Oper(this.OperFindNextDelegate);
            this.arrProc[-this.OP_PUSH] = new Oper(this.OperPush);
            this.arrProc[-this.OP_RET] = new Oper(this.OperRet);
            this.arrProc[-this.OP_EXIT_SUB] = new Oper(this.OperExitSub);
            this.arrProc[-this.OP_GET_PARAM_VALUE] = new Oper(this.OperGetParamValue);
            this.arrProc[-this.OP_CAST] = new Oper(this.OperCast);
            this.arrProc[-this.OP_TO_SBYTE] = new Oper(this.OperToSbyte);
            this.arrProc[-this.OP_TO_BYTE] = new Oper(this.OperToByte);
            this.arrProc[-this.OP_TO_USHORT] = new Oper(this.OperToUshort);
            this.arrProc[-this.OP_TO_SHORT] = new Oper(this.OperToShort);
            this.arrProc[-this.OP_TO_UINT] = new Oper(this.OperToUint);
            this.arrProc[-this.OP_TO_INT] = new Oper(this.OperToInt);
            this.arrProc[-this.OP_TO_ULONG] = new Oper(this.OperToUlong);
            this.arrProc[-this.OP_TO_LONG] = new Oper(this.OperToLong);
            this.arrProc[-this.OP_TO_CHAR] = new Oper(this.OperToChar);
            this.arrProc[-this.OP_TO_FLOAT] = new Oper(this.OperToFloat);
            this.arrProc[-this.OP_TO_DOUBLE] = new Oper(this.OperToDouble);
            this.arrProc[-this.OP_TO_DECIMAL] = new Oper(this.OperToDecimal);
            this.arrProc[-this.OP_TO_STRING] = new Oper(this.OperToString);
            this.arrProc[-this.OP_TO_BOOLEAN] = new Oper(this.OperToBoolean);
            this.arrProc[-this.OP_TO_ENUM] = new Oper(this.OperToEnum);
            this.arrProc[-this.OP_TO_CHAR_ARRAY] = new Oper(this.OperToCharArray);
            this.arrProc[-this.OP_TRY_ON] = new Oper(this.OperTryOn);
            this.arrProc[-this.OP_TRY_OFF] = new Oper(this.OperTryOff);
            this.arrProc[-this.OP_THROW] = new Oper(this.OperThrow);
            this.arrProc[-this.OP_CATCH] = new Oper(this.OperCatch);
            this.arrProc[-this.OP_FINALLY] = new Oper(this.OperFinally);
            this.arrProc[-this.OP_DISCARD_ERROR] = new Oper(this.OperDiscardError);
            this.arrProc[-this.OP_EXIT_ON_ERROR] = new Oper(this.OperExitOnError);
            this.arrProc[-this.OP_ONERROR] = new Oper(this.OperOnError);
            this.arrProc[-this.OP_RESUME] = new Oper(this.OperResume);
            this.arrProc[-this.OP_RESUME_NEXT] = new Oper(this.OperResumeNext);
            this.arrProc[-this.OP_SEPARATOR] = new Oper(this.OperNop);
            this.arrProc[-this.OP_DEFINE] = new Oper(this.OperNop);
            this.arrProc[-this.OP_UNDEF] = new Oper(this.OperNop);
            this.arrProc[-this.OP_START_REGION] = new Oper(this.OperNop);
            this.arrProc[-this.OP_END_REGION] = new Oper(this.OperNop);
            this.arrProc[-this.OP_NEGATION_INT] = new Oper(this.OperNegationInt);
            this.arrProc[-this.OP_NEGATION_LONG] = new Oper(this.OperNegationLong);
            this.arrProc[-this.OP_NEGATION_FLOAT] = new Oper(this.OperNegationFloat);
            this.arrProc[-this.OP_NEGATION_DOUBLE] = new Oper(this.OperNegationDouble);
            this.arrProc[-this.OP_NEGATION_DECIMAL] = new Oper(this.OperNegationDecimal);
            this.arrProc[-this.OP_LOGICAL_NEGATION_BOOL] = new Oper(this.OperLogicalNegationBool);
            this.arrProc[-this.OP_BITWISE_COMPLEMENT_INT] = new Oper(this.OperBitwiseComplementInt);
            this.arrProc[-this.OP_BITWISE_COMPLEMENT_UINT] = new Oper(this.OperBitwiseComplementUint);
            this.arrProc[-this.OP_BITWISE_COMPLEMENT_LONG] = new Oper(this.OperBitwiseComplementLong);
            this.arrProc[-this.OP_BITWISE_COMPLEMENT_ULONG] = new Oper(this.OperBitwiseComplementUlong);
            this.arrProc[-this.OP_INC_SBYTE] = new Oper(this.OperIncSbyte);
            this.arrProc[-this.OP_INC_BYTE] = new Oper(this.OperIncByte);
            this.arrProc[-this.OP_INC_SHORT] = new Oper(this.OperIncShort);
            this.arrProc[-this.OP_INC_USHORT] = new Oper(this.OperIncUshort);
            this.arrProc[-this.OP_INC_INT] = new Oper(this.OperIncInt);
            this.arrProc[-this.OP_INC_UINT] = new Oper(this.OperIncUint);
            this.arrProc[-this.OP_INC_LONG] = new Oper(this.OperIncLong);
            this.arrProc[-this.OP_INC_ULONG] = new Oper(this.OperIncUlong);
            this.arrProc[-this.OP_INC_CHAR] = new Oper(this.OperIncChar);
            this.arrProc[-this.OP_INC_FLOAT] = new Oper(this.OperIncFloat);
            this.arrProc[-this.OP_INC_DOUBLE] = new Oper(this.OperIncDouble);
            this.arrProc[-this.OP_INC_DECIMAL] = new Oper(this.OperIncDecimal);
            this.arrProc[-this.OP_DEC_SBYTE] = new Oper(this.OperDecSbyte);
            this.arrProc[-this.OP_DEC_BYTE] = new Oper(this.OperDecByte);
            this.arrProc[-this.OP_DEC_SHORT] = new Oper(this.OperDecShort);
            this.arrProc[-this.OP_DEC_USHORT] = new Oper(this.OperDecUshort);
            this.arrProc[-this.OP_DEC_INT] = new Oper(this.OperDecInt);
            this.arrProc[-this.OP_DEC_UINT] = new Oper(this.OperDecUint);
            this.arrProc[-this.OP_DEC_LONG] = new Oper(this.OperDecLong);
            this.arrProc[-this.OP_DEC_ULONG] = new Oper(this.OperDecUlong);
            this.arrProc[-this.OP_DEC_CHAR] = new Oper(this.OperDecChar);
            this.arrProc[-this.OP_DEC_FLOAT] = new Oper(this.OperDecFloat);
            this.arrProc[-this.OP_DEC_DOUBLE] = new Oper(this.OperDecDouble);
            this.arrProc[-this.OP_DEC_DECIMAL] = new Oper(this.OperDecDecimal);
            this.arrProc[-this.OP_ADDITION_INT] = new Oper(this.OperAdditionInt);
            this.arrProc[-this.OP_ADDITION_UINT] = new Oper(this.OperAdditionUint);
            this.arrProc[-this.OP_ADDITION_LONG] = new Oper(this.OperAdditionLong);
            this.arrProc[-this.OP_ADDITION_ULONG] = new Oper(this.OperAdditionUlong);
            this.arrProc[-this.OP_ADDITION_FLOAT] = new Oper(this.OperAdditionFloat);
            this.arrProc[-this.OP_ADDITION_DOUBLE] = new Oper(this.OperAdditionDouble);
            this.arrProc[-this.OP_ADDITION_DECIMAL] = new Oper(this.OperAdditionDecimal);
            this.arrProc[-this.OP_ADDITION_STRING] = new Oper(this.OperAdditionString);
            this.arrProc[-this.OP_SUBTRACTION_INT] = new Oper(this.OperSubtractionInt);
            this.arrProc[-this.OP_SUBTRACTION_UINT] = new Oper(this.OperSubtractionUint);
            this.arrProc[-this.OP_SUBTRACTION_LONG] = new Oper(this.OperSubtractionLong);
            this.arrProc[-this.OP_SUBTRACTION_ULONG] = new Oper(this.OperSubtractionUlong);
            this.arrProc[-this.OP_SUBTRACTION_FLOAT] = new Oper(this.OperSubtractionFloat);
            this.arrProc[-this.OP_SUBTRACTION_DOUBLE] = new Oper(this.OperSubtractionDouble);
            this.arrProc[-this.OP_SUBTRACTION_DECIMAL] = new Oper(this.OperSubtractionDecimal);
            this.arrProc[-this.OP_MULTIPLICATION_INT] = new Oper(this.OperMultiplicationInt);
            this.arrProc[-this.OP_MULTIPLICATION_UINT] = new Oper(this.OperMultiplicationUint);
            this.arrProc[-this.OP_MULTIPLICATION_LONG] = new Oper(this.OperMultiplicationLong);
            this.arrProc[-this.OP_MULTIPLICATION_ULONG] = new Oper(this.OperMultiplicationUlong);
            this.arrProc[-this.OP_MULTIPLICATION_FLOAT] = new Oper(this.OperMultiplicationFloat);
            this.arrProc[-this.OP_MULTIPLICATION_DOUBLE] = new Oper(this.OperMultiplicationDouble);
            this.arrProc[-this.OP_MULTIPLICATION_DECIMAL] = new Oper(this.OperMultiplicationDecimal);
            this.arrProc[-this.OP_EXPONENT_INT] = new Oper(this.OperExponentInt);
            this.arrProc[-this.OP_EXPONENT_UINT] = new Oper(this.OperExponentUint);
            this.arrProc[-this.OP_EXPONENT_LONG] = new Oper(this.OperExponentLong);
            this.arrProc[-this.OP_EXPONENT_ULONG] = new Oper(this.OperExponentUlong);
            this.arrProc[-this.OP_EXPONENT_FLOAT] = new Oper(this.OperExponentFloat);
            this.arrProc[-this.OP_EXPONENT_DOUBLE] = new Oper(this.OperExponentDouble);
            this.arrProc[-this.OP_EXPONENT_DECIMAL] = new Oper(this.OperExponentDecimal);
            this.arrProc[-this.OP_DIVISION_INT] = new Oper(this.OperDivisionInt);
            this.arrProc[-this.OP_DIVISION_UINT] = new Oper(this.OperDivisionUint);
            this.arrProc[-this.OP_DIVISION_LONG] = new Oper(this.OperDivisionLong);
            this.arrProc[-this.OP_DIVISION_ULONG] = new Oper(this.OperDivisionUlong);
            this.arrProc[-this.OP_DIVISION_FLOAT] = new Oper(this.OperDivisionFloat);
            this.arrProc[-this.OP_DIVISION_DOUBLE] = new Oper(this.OperDivisionDouble);
            this.arrProc[-this.OP_DIVISION_DECIMAL] = new Oper(this.OperDivisionDecimal);
            this.arrProc[-this.OP_REMAINDER_INT] = new Oper(this.OperRemainderInt);
            this.arrProc[-this.OP_REMAINDER_UINT] = new Oper(this.OperRemainderUint);
            this.arrProc[-this.OP_REMAINDER_LONG] = new Oper(this.OperRemainderLong);
            this.arrProc[-this.OP_REMAINDER_ULONG] = new Oper(this.OperRemainderUlong);
            this.arrProc[-this.OP_REMAINDER_FLOAT] = new Oper(this.OperRemainderFloat);
            this.arrProc[-this.OP_REMAINDER_DOUBLE] = new Oper(this.OperRemainderDouble);
            this.arrProc[-this.OP_REMAINDER_DECIMAL] = new Oper(this.OperRemainderDecimal);
            this.arrProc[-this.OP_LEFT_SHIFT_INT] = new Oper(this.OperLeftShiftInt);
            this.arrProc[-this.OP_LEFT_SHIFT_UINT] = new Oper(this.OperLeftShiftUint);
            this.arrProc[-this.OP_LEFT_SHIFT_LONG] = new Oper(this.OperLeftShiftLong);
            this.arrProc[-this.OP_LEFT_SHIFT_ULONG] = new Oper(this.OperLeftShiftUlong);
            this.arrProc[-this.OP_RIGHT_SHIFT_INT] = new Oper(this.OperRightShiftInt);
            this.arrProc[-this.OP_RIGHT_SHIFT_UINT] = new Oper(this.OperRightShiftUint);
            this.arrProc[-this.OP_RIGHT_SHIFT_LONG] = new Oper(this.OperRightShiftLong);
            this.arrProc[-this.OP_RIGHT_SHIFT_ULONG] = new Oper(this.OperRightShiftUlong);
            this.arrProc[-this.OP_BITWISE_AND_INT] = new Oper(this.OperBitwiseAndInt);
            this.arrProc[-this.OP_BITWISE_AND_UINT] = new Oper(this.OperBitwiseAndUint);
            this.arrProc[-this.OP_BITWISE_AND_LONG] = new Oper(this.OperBitwiseAndLong);
            this.arrProc[-this.OP_BITWISE_AND_ULONG] = new Oper(this.OperBitwiseAndUlong);
            this.arrProc[-this.OP_BITWISE_AND_BOOL] = new Oper(this.OperBitwiseAndBool);
            this.arrProc[-this.OP_BITWISE_OR_INT] = new Oper(this.OperBitwiseOrInt);
            this.arrProc[-this.OP_BITWISE_OR_UINT] = new Oper(this.OperBitwiseOrUint);
            this.arrProc[-this.OP_BITWISE_OR_LONG] = new Oper(this.OperBitwiseOrLong);
            this.arrProc[-this.OP_BITWISE_OR_ULONG] = new Oper(this.OperBitwiseOrUlong);
            this.arrProc[-this.OP_BITWISE_OR_BOOL] = new Oper(this.OperBitwiseOrBool);
            this.arrProc[-this.OP_BITWISE_XOR_INT] = new Oper(this.OperBitwiseXorInt);
            this.arrProc[-this.OP_BITWISE_XOR_UINT] = new Oper(this.OperBitwiseXorUint);
            this.arrProc[-this.OP_BITWISE_XOR_LONG] = new Oper(this.OperBitwiseXorLong);
            this.arrProc[-this.OP_BITWISE_XOR_ULONG] = new Oper(this.OperBitwiseXorUlong);
            this.arrProc[-this.OP_BITWISE_XOR_BOOL] = new Oper(this.OperBitwiseXorBool);
            this.arrProc[-this.OP_LT_INT] = new Oper(this.OperLessThanInt);
            this.arrProc[-this.OP_LT_UINT] = new Oper(this.OperLessThanUint);
            this.arrProc[-this.OP_LT_LONG] = new Oper(this.OperLessThanLong);
            this.arrProc[-this.OP_LT_ULONG] = new Oper(this.OperLessThanUlong);
            this.arrProc[-this.OP_LT_FLOAT] = new Oper(this.OperLessThanFloat);
            this.arrProc[-this.OP_LT_DOUBLE] = new Oper(this.OperLessThanDouble);
            this.arrProc[-this.OP_LT_DECIMAL] = new Oper(this.OperLessThanDecimal);
            this.arrProc[-this.OP_LT_STRING] = new Oper(this.OperLessThanString);
            this.arrProc[-this.OP_LE_INT] = new Oper(this.OperLessThanOrEqualInt);
            this.arrProc[-this.OP_LE_UINT] = new Oper(this.OperLessThanOrEqualUint);
            this.arrProc[-this.OP_LE_LONG] = new Oper(this.OperLessThanOrEqualLong);
            this.arrProc[-this.OP_LE_ULONG] = new Oper(this.OperLessThanOrEqualUlong);
            this.arrProc[-this.OP_LE_FLOAT] = new Oper(this.OperLessThanOrEqualFloat);
            this.arrProc[-this.OP_LE_DOUBLE] = new Oper(this.OperLessThanOrEqualDouble);
            this.arrProc[-this.OP_LE_DECIMAL] = new Oper(this.OperLessThanOrEqualDecimal);
            this.arrProc[-this.OP_LE_STRING] = new Oper(this.OperLessThanOrEqualString);
            this.arrProc[-this.OP_GT_INT] = new Oper(this.OperGreaterThanInt);
            this.arrProc[-this.OP_GT_UINT] = new Oper(this.OperGreaterThanUint);
            this.arrProc[-this.OP_GT_LONG] = new Oper(this.OperGreaterThanLong);
            this.arrProc[-this.OP_GT_ULONG] = new Oper(this.OperGreaterThanUlong);
            this.arrProc[-this.OP_GT_FLOAT] = new Oper(this.OperGreaterThanFloat);
            this.arrProc[-this.OP_GT_DOUBLE] = new Oper(this.OperGreaterThanDouble);
            this.arrProc[-this.OP_GT_DECIMAL] = new Oper(this.OperGreaterThanDecimal);
            this.arrProc[-this.OP_GT_STRING] = new Oper(this.OperGreaterThanString);
            this.arrProc[-this.OP_GE_INT] = new Oper(this.OperGreaterThanOrEqualInt);
            this.arrProc[-this.OP_GE_UINT] = new Oper(this.OperGreaterThanOrEqualUint);
            this.arrProc[-this.OP_GE_LONG] = new Oper(this.OperGreaterThanOrEqualLong);
            this.arrProc[-this.OP_GE_ULONG] = new Oper(this.OperGreaterThanOrEqualUlong);
            this.arrProc[-this.OP_GE_FLOAT] = new Oper(this.OperGreaterThanOrEqualFloat);
            this.arrProc[-this.OP_GE_DOUBLE] = new Oper(this.OperGreaterThanOrEqualDouble);
            this.arrProc[-this.OP_GE_DECIMAL] = new Oper(this.OperGreaterThanOrEqualDecimal);
            this.arrProc[-this.OP_GE_STRING] = new Oper(this.OperGreaterThanOrEqualString);
            this.arrProc[-this.OP_EQ_INT] = new Oper(this.OperEqualityInt);
            this.arrProc[-this.OP_EQ_UINT] = new Oper(this.OperEqualityUint);
            this.arrProc[-this.OP_EQ_LONG] = new Oper(this.OperEqualityLong);
            this.arrProc[-this.OP_EQ_ULONG] = new Oper(this.OperEqualityUlong);
            this.arrProc[-this.OP_EQ_FLOAT] = new Oper(this.OperEqualityFloat);
            this.arrProc[-this.OP_EQ_DOUBLE] = new Oper(this.OperEqualityDouble);
            this.arrProc[-this.OP_EQ_DECIMAL] = new Oper(this.OperEqualityDecimal);
            this.arrProc[-this.OP_EQ_STRING] = new Oper(this.OperEqualityString);
            this.arrProc[-this.OP_EQ_BOOL] = new Oper(this.OperEqualityBool);
            this.arrProc[-this.OP_EQ_OBJECT] = new Oper(this.OperEqualityObject);
            this.arrProc[-this.OP_NE_INT] = new Oper(this.OperInequalityInt);
            this.arrProc[-this.OP_NE_UINT] = new Oper(this.OperInequalityUint);
            this.arrProc[-this.OP_NE_LONG] = new Oper(this.OperInequalityLong);
            this.arrProc[-this.OP_NE_ULONG] = new Oper(this.OperInequalityUlong);
            this.arrProc[-this.OP_NE_FLOAT] = new Oper(this.OperInequalityFloat);
            this.arrProc[-this.OP_NE_DOUBLE] = new Oper(this.OperInequalityDouble);
            this.arrProc[-this.OP_NE_DECIMAL] = new Oper(this.OperInequalityDecimal);
            this.arrProc[-this.OP_NE_STRING] = new Oper(this.OperInequalityString);
            this.arrProc[-this.OP_NE_BOOL] = new Oper(this.OperInequalityBool);
            this.arrProc[-this.OP_NE_OBJECT] = new Oper(this.OperInequalityObject);
            this.arrProc[-this.OP_SWAPPED_ARGUMENTS] = new Oper(this.OperSwappedArguments);
            this.prog = new ArrayList();
            for (num = 0; num < 0x3e8; num++)
            {
                this.prog.Add(new ProgRec());
            }
            this.n = 0;
            this.card = 0;
        }

        public void AddBreakpoint(Breakpoint bp)
        {
            this.breakpoint_list.Add(bp);
        }

        private int AddInstruction(int op, int arg1, int arg2, int res)
        {
            this.Card++;
            this.SetInstruction(this.Card, op, arg1, arg2, res);
            return this.Card;
        }

        public void AdjustCalls()
        {
            this.AdjustCallsEx(0);
        }

        public void AdjustCallsEx(int init_n)
        {
            if (this.scripter.IsError())
            {
                return;
            }
            for (int i = init_n + 1; i <= this.symbol_table.Card; i++)
            {
                switch (this.symbol_table[i].Kind)
                {
                    case MemberKind.Method:
                    case MemberKind.Constructor:
                    case MemberKind.Destructor:
                        this.GetFunctionObject(i).CreateSignature();
                        break;
                }
            }
            this.n = init_n;
        Label_0062:
            this.n++;
            if (this.n < this.Card)
            {
                if (this[this.n].op == this.OP_CALL_SIMPLE)
                {
                    this[this.n].op = this.OP_CALL;
                }
                if (this[this.n].op != this.OP_CALL)
                {
                    goto Label_0062;
                }
                int id = this[this.n].arg1;
                if (this.symbol_table[id].Kind != MemberKind.Method)
                {
                    goto Label_0062;
                }
                FunctionObject functionObject = this.GetFunctionObject(id);
                if ((functionObject.HasModifier(Modifier.Virtual) || functionObject.HasModifier(Modifier.Override)) || functionObject.HasModifier(Modifier.Abstract))
                {
                    this[this.n].op = this.OP_CALL_VIRT;
                }
                if (functionObject.ParamCount != 1)
                {
                    goto Label_0062;
                }
                if ((functionObject.Imported && (functionObject.Name.Length > 4)) && (functionObject.Name.Substring(0, 4) == "add_"))
                {
                    int typeId = this.symbol_table[functionObject.Param_Ids[0]].TypeId;
                    ClassObject classObject = this.GetClassObject(typeId);
                    if (classObject.IsDelegate)
                    {
                        string name = functionObject.Name.Substring(4);
                        EventInfo e = functionObject.Owner.ImportedType.GetEvent(name);
                        FunctionObject patternMethod = classObject.PatternMethod;
                        if ((e != null) && (patternMethod != null))
                        {
                            this.scripter.DefineEventHandler(e, patternMethod);
                            this[this.n].op = this.OP_CALL_ADD_EVENT;
                        }
                    }
                    goto Label_0062;
                }
                if (functionObject.GetParamTypeId(0) != 0x10)
                {
                    goto Label_0062;
                }
                string fullName = functionObject.FullName;
                if (!(fullName == "System.Console.Write") && !(fullName == "System.Console.WriteLine"))
                {
                    goto Label_0062;
                }
                int n = this.n;
                do
                {
                    n--;
                    if ((this[n].op == this.OP_PUSH) && (this[n].res == functionObject.Id))
                    {
                        FunctionObject obj7;
                        this.r = this[n];
                        int num5 = this.GetTypeId(this.r.arg1);
                        ClassObject obj6 = this.GetClassObject(num5);
                        bool upcase = this.GetUpcase(this.n);
                        id = obj6.FindMethodId("ToString", null, null, 0, out obj7, upcase);
                        if (id > 0)
                        {
                            int num6 = this.AppVar(this.symbol_table[this.r.arg1].Level, 12);
                            this.InsertOperators(n, 2);
                            this[n].op = this.OP_PUSH;
                            this[n].arg1 = this.r.arg1;
                            this[n].arg2 = 0;
                            this[n].arg2 = 0;
                            n++;
                            this[n].op = this.OP_CALL;
                            this[n].arg1 = id;
                            this[n].arg2 = 0;
                            this[n].res = num6;
                            this.r.arg1 = num6;
                            this.n++;
                            this.n++;
                        }
                        goto Label_0062;
                    }
                }
                while (n != 0);
                this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
            }
        }

        public int AppLabel()
        {
            return this.symbol_table.AppLabel();
        }

        private int AppVar(int level)
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Level = level;
            return num;
        }

        private int AppVar(int level, int type_id)
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Level = level;
            this.symbol_table[num].TypeId = type_id;
            return num;
        }

        public object CallMethod(RunMode rm, object target, int sub_id, params object[] p)
        {
            object obj2 = null;
            if (sub_id != this.scripter.EntryId)
            {
                this.SaveState();
            }
            int length = 0;
            int resultId = this.symbol_table.GetResultId(sub_id);
            FunctionObject functionObject = this.GetFunctionObject(sub_id);
            this.n = this.Card;
            if ((p != null) && (functionObject.ParamCount > 0))
            {
                length = p.Length;
                for (int i = 0; i < length; i++)
                {
                    this.stack.Push(p[i]);
                }
            }
            this.stack.Push(target);
            this.Card++;
            this.SetInstruction(this.Card, this.OP_CALL, sub_id, length, resultId);
            this.Card++;
            this.SetInstruction(this.Card, this.OP_HALT, sub_id, 0, 0);
            this.Run(rm);
            obj2 = this.symbol_table[resultId].Value;
            if (sub_id != this.scripter.EntryId)
            {
                this.RestoreState();
            }
            return obj2;
        }

        public object CallMethodEx(RunMode rm, object target, int sub_id, params object[] p)
        {
            object obj2 = null;
            this.SaveState();
            int length = 0;
            int resultId = this.symbol_table.GetResultId(sub_id);
            this.n = this.Card;
            if (p != null)
            {
                length = p.Length;
                for (int i = 0; i < length; i++)
                {
                    this.stack.Push(p[i]);
                }
            }
            this.stack.Push(target);
            this.Card++;
            this.SetInstruction(this.Card, this.OP_CALL, sub_id, length, resultId);
            this.Card++;
            this.SetInstruction(this.Card, this.OP_HALT, 0, 0, 0);
            this.Run(rm);
            obj2 = this.symbol_table[resultId].Value;
            if (this.Paused)
            {
                int n = this.n;
                this.RestoreState();
                this.n = n;
                return obj2;
            }
            this.RestoreState();
            return obj2;
        }

        public void CallStaticConstructors()
        {
            for (int i = 1; i <= this.symbol_table.Card; i++)
            {
                if (this.symbol_table[i].Kind == MemberKind.Constructor)
                {
                    FunctionObject functionObject = this.GetFunctionObject(i);
                    if (functionObject.Static && !functionObject.Imported)
                    {
                        this.CallMethod(RunMode.Run, null, functionObject.Id, null);
                    }
                }
            }
        }

        private void CheckOP_ASSIGN(ClassObject enum_class)
        {
            ClassObject obj3;
            if (this.symbol_table[this.r.arg1].Kind == MemberKind.Index)
            {
                int level = this.symbol_table[this.r.arg1].Level;
                if (this.symbol_table[level].TypeId == 12)
                {
                    this.scripter.CreateErrorObjectEx("CS0200. Property or indexer '{0}' cannot be assigned to — it is read only.", new object[] { "this" });
                    return;
                }
            }
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            if (classObject.IsSubrange)
            {
                typeId = classObject.AncestorIds[0];
            }
            if (typeId != id)
            {
                if (id == 0)
                {
                    id = 0;
                }
                else if (typeId != 0x10)
                {
                    if (this.r.arg2 == this.symbol_table.NULL_id)
                    {
                        classObject = this.GetClassObject(typeId);
                        if (classObject.IsValueType)
                        {
                            this.scripter.CreateErrorObjectEx("CS0037. Cannot convert null to '{0}' because it is a value type.", new object[] { classObject.Name });
                            return;
                        }
                    }
                    else if (enum_class != null)
                    {
                        int num4 = enum_class.UnderlyingType.Id;
                        classObject = this.GetClassObject(num4);
                        obj3 = this.GetClassObject(id);
                        if (!this.scripter.MatchTypes(obj3, classObject))
                        {
                            this.scripter.CreateErrorObjectEx("CS0029. Cannot impllicitly convert type '{0}' to '{1}'.", new object[] { obj3.Name, classObject.Name });
                            return;
                        }
                        this.InsertNumericConversion(num4, 2);
                    }
                    else if (typeId == 0)
                    {
                        this.symbol_table[this.r.arg1].TypeId = id;
                        if (id == 0)
                        {
                            this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                            return;
                        }
                    }
                    else
                    {
                        classObject = this.GetClassObject(typeId);
                        obj3 = this.GetClassObject(id);
                        bool flag = obj3.InheritsFrom(classObject);
                        if (!flag)
                        {
                            flag = this.scripter.conversion.ExistsImplicitNumericConstConversion(this.scripter, this.r.arg2, this.r.arg1);
                        }
                        if (!flag)
                        {
                            flag = this.scripter.MatchTypes(obj3, classObject);
                        }
                        if (!flag)
                        {
                            ClassObject obj4;
                            int num5 = classObject.FindOverloadableImplicitOperatorId(this.r.arg2, this.r.res);
                            if (num5 == 0)
                            {
                                num5 = obj3.FindOverloadableImplicitOperatorId(this.r.arg2, this.r.res);
                                obj4 = obj3;
                            }
                            else
                            {
                                obj4 = classObject;
                            }
                            flag = num5 > 0;
                            if (flag)
                            {
                                this.InsertOperators(this.n, 2);
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = this.r.arg2;
                                this[this.n].arg2 = 0;
                                this[this.n].res = num5;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = obj4.Id;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.n++;
                                this[this.n].op = this.OP_CALL_SIMPLE;
                                this[this.n].arg1 = num5;
                                this[this.n].arg2 = 1;
                                this[this.n].res = this.r.res;
                                return;
                            }
                        }
                        if (!flag)
                        {
                            this.scripter.CreateErrorObjectEx("CS0029. Cannot impllicitly convert type '{0}' to '{1}'.", new object[] { obj3.Name, classObject.Name });
                            return;
                        }
                    }
                }
            }
            classObject = this.GetClassObject(typeId);
            obj3 = this.GetClassObject(id);
            if ((typeId > this.symbol_table.OBJECT_CLASS_id) && (classObject.Class_Kind == ClassKind.Struct))
            {
                this.r.op = this.OP_ASSIGN_STRUCT;
            }
            else if ((IsNumericTypeId(classObject.Id) && IsNumericTypeId(obj3.Id)) && ((this.symbol_table[this.r.arg1].Kind != MemberKind.Var) || (classObject != obj3)))
            {
                this.InsertNumericConversion(classObject.Id, 2);
            }
        }

        private void CheckOP_ASSIGN_COND_TYPE()
        {
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int num2 = this.symbol_table[this.r.arg2].TypeId;
            int num3 = 0;
            if (typeId == num2)
            {
                num3 = typeId;
            }
            else if (this.scripter.conversion.ExistsImplicitConversion(this.scripter, this.r.arg1, this.r.arg2) && !this.scripter.conversion.ExistsImplicitConversion(this.scripter, this.r.arg2, this.r.arg1))
            {
                num3 = num2;
            }
            else if (this.scripter.conversion.ExistsImplicitConversion(this.scripter, this.r.arg2, this.r.arg1) && !this.scripter.conversion.ExistsImplicitConversion(this.scripter, this.r.arg1, this.r.arg2))
            {
                num3 = typeId;
            }
            if (num3 == 0)
            {
                string name = this.symbol_table[typeId].Name;
                string str2 = this.symbol_table[num2].Name;
                this.scripter.CreateErrorObjectEx("CS0173. Type of conditional expression can't be determined because there is no implicit conversion between '{0}' and '{1}'.", new object[] { name, str2 });
            }
            this.symbol_table[this.r.res].TypeId = num3;
        }

        private void CheckOP_BITWISE_AND()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "&", this.detailed_bitwise_and_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_BITWISE_OR()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "|", this.detailed_bitwise_or_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_BITWISE_XOR()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "^", this.detailed_bitwise_xor_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_CALL(ClassObject current_class, int curr_level, IntegerList a, IntegerList param_mod, IntegerList pos)
        {
            FunctionObject obj10;
            int num35;
            bool flag8;
            int id = this.r.arg1;
            int n = this.n;
            a.Clear();
            param_mod.Clear();
            pos.Clear();
            int num3 = 0;
            int avalue = this.n;
            while (true)
            {
                if (avalue <= 1)
                {
                    this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                    return;
                }
                if ((this[avalue].op == this.OP_BEGIN_CALL) && (this[avalue].arg1 == id))
                {
                    break;
                }
                avalue--;
            }
            this[avalue].op = this.OP_NOP;
            while (true)
            {
                if ((this[avalue].op == this.OP_PUSH) && (this[avalue].res == id))
                {
                    pos.Add(avalue);
                    a.Add(this[avalue].arg1);
                    param_mod.Add(this[avalue].arg2);
                    if (this[avalue].arg2 > 0)
                    {
                        num3++;
                    }
                }
                if (avalue == this.n)
                {
                    break;
                }
                avalue++;
            }
            this.scripter.Dump();
            MemberKind kind = this.symbol_table[id].Kind;
            if (kind == MemberKind.Type)
            {
                ClassObject val = (ClassObject) this.GetVal(id);
                if (val.IsDelegate)
                {
                    if (a.Count == 1)
                    {
                        FunctionObject obj3 = this.GetFunctionObject(curr_level);
                        this.InsertOperators(pos[0], 1);
                        this.n++;
                        this[pos[0]].op = this.OP_PUSH;
                        this[pos[0]].arg1 = obj3.ThisId;
                        this[pos[0]].arg2 = 0;
                        this[pos[0]].res = val.Id;
                        a.Insert(0, obj3.ThisId);
                    }
                    if (val.PatternMethod.Init == null)
                    {
                        FunctionObject patternMethod = val.PatternMethod;
                        int last = a.Last;
                        if (this.symbol_table[last].Kind == MemberKind.Ref)
                        {
                            int num6 = this.symbol_table[last].Level;
                            int num7 = this.symbol_table[num6].TypeId;
                            ClassObject classObjectEx = this.GetClassObjectEx(num7);
                            int num8 = this.symbol_table[last].NameIndex;
                            bool flag = this.GetUpcase(this.n);
                            MemberObject memberByNameIndex = classObjectEx.GetMemberByNameIndex(num8, flag);
                            this.ReplaceId(a.Last, memberByNameIndex.Id);
                            a.Last = memberByNameIndex.Id;
                        }
                        FunctionObject g = this.GetFunctionObject(a.Last);
                        this.CreatePatternMethod(patternMethod, g);
                    }
                    this[this.n].op = this.OP_SETUP_DELEGATE;
                    this[this.n].arg1 = 0;
                    this[this.n].arg2 = 0;
                    this[this.n].res = 0;
                }
                else if (this.r.res > 0)
                {
                    if ((a.Count == 1) && (this.GetLanguage(this.n) == CSLite_Language.Pascal))
                    {
                        this.r.op = this.OP_CAST;
                        this.r.arg2 = a[0];
                        this[this.n - 1].op = this.OP_NOP;
                        this[pos[0]].op = this.OP_NOP;
                        this.n--;
                    }
                    else
                    {
                        string str = this.symbol_table[this.r.arg1].Name;
                        this.scripter.CreateErrorObjectEx("CS0119. '{0}' denotes a '{1}' which is not valid in the given context.", new object[] { str, "type" });
                    }
                }
                else
                {
                    FunctionObject obj8;
                    id = val.FindConstructorId(a, param_mod, out obj8);
                    if (id != 0)
                    {
                        goto Label_17D6;
                    }
                    string str2 = val.Name;
                    if (obj8 != null)
                    {
                        if (obj8.ParamCount == a.Count)
                        {
                            if ((obj8.ParamsId == 0) || (a.Count == 0))
                            {
                                this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { str2 });
                                return;
                            }
                            int num9 = a[a.Count - 1];
                            int num10 = this.symbol_table[num9].TypeId;
                            if (CSLite_System.GetRank(this.symbol_table[num10].Name) != 0)
                            {
                                this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { str2 });
                                return;
                            }
                            this.InsertActualArray(obj8, curr_level, pos, a);
                            id = obj8.Id;
                            n = this.n;
                            goto Label_17D6;
                        }
                        if (a.Count < obj8.ParamCount)
                        {
                            if (obj8.ParamsId == 0)
                            {
                                this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { str2, a.Count });
                            }
                            else
                            {
                                if ((a.Count + 1) == obj8.ParamCount)
                                {
                                    this.InsertActualArray(obj8, curr_level, pos, a);
                                    id = obj8.Id;
                                    n = this.n;
                                    goto Label_17D6;
                                }
                                this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { str2, a.Count });
                            }
                        }
                        else
                        {
                            this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { str2, a.Count });
                        }
                    }
                    else
                    {
                        this.scripter.CreateErrorObjectEx("CS0143. The type '{0}' has no constructors defined.", new object[] { val.Name });
                    }
                }
                return;
            }
            if (kind != MemberKind.Method)
            {
                switch (kind)
                {
                    case MemberKind.Constructor:
                    {
                        FunctionObject obj13;
                        int num32 = this.symbol_table[id].Level;
                        ClassObject obj12 = this.GetClassObject(num32);
                        id = obj12.FindConstructorId(a, param_mod, out obj13);
                        this.r.res = 0;
                        if (id == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0143. The type '{0}' has no constructors defined.", new object[] { obj12.Name });
                        }
                        goto Label_17D6;
                    }
                    case MemberKind.Destructor:
                    {
                        int num33 = this.symbol_table[id].Level;
                        ClassObject obj14 = this.GetClassObject(num33);
                        id = obj14.FindDestructorId(a);
                        this.r.res = 0;
                        if (id != 0)
                        {
                            goto Label_17D6;
                        }
                        this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { obj14.Name, "~" + obj14.Name });
                        return;
                    }
                }
                if (kind != MemberKind.Ref)
                {
                    switch (kind)
                    {
                        case MemberKind.Var:
                        case MemberKind.Index:
                        {
                            int num55 = this.symbol_table[this.r.arg1].TypeId;
                            ClassObject obj20 = this.GetClassObject(num55);
                            if (((kind == MemberKind.Var) && (obj20.IsArray || obj20.IsPascalArray)) && this.PascalOrBasic(this.n))
                            {
                                this.ConvertCallToIndexAccess(a, pos);
                                return;
                            }
                            if (((kind == MemberKind.Var) && (obj20.DefaultProperty != null)) && this.PascalOrBasic(this.n))
                            {
                                this.ConvertToDefaultPropertyCall(obj20.DefaultProperty, a, pos);
                                return;
                            }
                            if ((kind == MemberKind.Property) && this.PascalOrBasic(this.n))
                            {
                                this.ConvertToPropertyCall(a, pos);
                                return;
                            }
                            if (!obj20.IsDelegate)
                            {
                                if (this.PascalOrBasic(this.n))
                                {
                                    this.ConvertCallToIndexAccess(a, pos);
                                }
                                else
                                {
                                    string str9 = this.symbol_table[this.r.arg1].Name;
                                    this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { str9, "variable", "method" });
                                }
                                return;
                            }
                            if (this.PascalOrBasic(this.n))
                            {
                                this[this.n - 1].arg1 = this.r.arg1;
                            }
                            id = obj20.PatternMethod.Id;
                            goto Label_17D6;
                        }
                    }
                    int num56 = this.symbol_table[this.r.arg1].TypeId;
                    ClassObject obj21 = this.GetClassObject(num56);
                    if (((kind == MemberKind.Field) && (obj21.IsArray || obj21.IsPascalArray)) && this.PascalOrBasic(this.n))
                    {
                        this.ConvertCallToIndexAccess(a, pos);
                        return;
                    }
                    if (((kind == MemberKind.Field) && (obj21.DefaultProperty != null)) && this.PascalOrBasic(this.n))
                    {
                        this.ConvertToDefaultPropertyCall(obj21.DefaultProperty, a, pos);
                        return;
                    }
                    if ((kind == MemberKind.Property) && this.PascalOrBasic(this.n))
                    {
                        this.ConvertToPropertyCall(a, pos);
                        return;
                    }
                    if (!obj21.IsDelegate)
                    {
                        string str10 = this.symbol_table[this.r.arg1].Name;
                        this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { str10, "variable", "method" });
                        return;
                    }
                    if ((kind == MemberKind.Field) && (obj21.ImportedType != null))
                    {
                        int num57 = this.symbol_table[this.r.arg1].Level;
                        if ((this.symbol_table[num57].Kind == MemberKind.Type) && (this.GetClassObject(num57).ImportedType != null))
                        {
                            this.r.op = this.OP_DYNAMIC_INVOKE;
                            return;
                        }
                    }
                    this[this.n - 1].arg1 = this.r.arg1;
                    id = obj21.PatternMethod.Id;
                }
                else
                {
                    FunctionObject obj16;
                    int num34 = this.symbol_table[id].Level;
                    num35 = this[this.n - 1].arg1;
                    this[this.n - 1].arg1 = num34;
                    int num36 = this.symbol_table[id].NameIndex;
                    string str6 = this.symbol_table[id].Name;
                    int num37 = this.symbol_table[num34].TypeId;
                    if (this.symbol_table[num37].Kind != MemberKind.Type)
                    {
                        string str7 = this.symbol_table[num37].Name;
                        this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { str7 });
                        return;
                    }
                    ClassObject obj15 = this.GetClassObjectEx(num37);
                    bool flag5 = this.GetUpcase(this.n);
                    id = obj15.FindMethodId(num36, a, param_mod, 0, out obj16, flag5);
                    if (id == 0)
                    {
                        if (obj16 == null)
                        {
                            int num51 = this.symbol_table[this.r.arg1].TypeId;
                            if (this.symbol_table[num51].Kind != MemberKind.Type)
                            {
                                this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { obj15.Name, str6 });
                                return;
                            }
                            ClassObject obj18 = this.GetClassObject(num51);
                            if ((obj18.IsArray || obj18.IsPascalArray) && this.PascalOrBasic(this.n))
                            {
                                this.ConvertCallToIndexAccess(a, pos);
                                return;
                            }
                            if ((obj18.DefaultProperty != null) && this.PascalOrBasic(this.n))
                            {
                                this.ConvertToDefaultPropertyCall(obj18.DefaultProperty, a, pos);
                                return;
                            }
                            if (!obj18.IsDelegate)
                            {
                                if (this.PascalOrBasic(this.n))
                                {
                                    this.ConvertCallToIndexAccess(a, pos);
                                }
                                else
                                {
                                    this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { obj15.Name, str6 });
                                }
                                return;
                            }
                            if (obj18.ImportedType != null)
                            {
                                int num52 = this.symbol_table[this.r.arg1].Level;
                                int num53 = this.symbol_table[num52].TypeId;
                                if (this.GetClassObject(num53).ImportedType != null)
                                {
                                    this.r.op = this.OP_DYNAMIC_INVOKE;
                                    return;
                                }
                            }
                            id = obj18.PatternMethod.Id;
                            if (!this.PascalOrBasic(this.n))
                            {
                                this[this.n - 1].arg1 = num35;
                                goto Label_17D6;
                            }
                            int num54 = this.n;
                            flag8 = false;
                            while (true)
                            {
                                num54--;
                                if ((this[num54].op == this.OP_RAISE_EVENT) && (this[num54].arg1 == this.r.arg1))
                                {
                                    flag8 = true;
                                    goto Label_143F;
                                }
                                if (num54 == 1)
                                {
                                    goto Label_143F;
                                }
                            }
                        }
                        if (obj16.ParamCount != a.Count)
                        {
                            if ((a.Count < obj16.ParamCount) || (obj16.ParamsId == 0))
                            {
                                if (obj16.Name == "Dispose")
                                {
                                    for (int i = 0; i < pos.Count; i++)
                                    {
                                        this[pos[i]].op = this.OP_NOP;
                                    }
                                    this[this.n - 1].op = this.OP_NOP;
                                    this[this.n].op = this.OP_NOP;
                                }
                                else
                                {
                                    this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { str6, a.Count });
                                }
                                return;
                            }
                            this.InsertActualArray(obj16, curr_level, pos, a);
                            id = obj16.Id;
                            n = this.n;
                        }
                        else
                        {
                            bool flag6 = true;
                            for (int j = 0; j < a.Count; j++)
                            {
                                int num39 = a[j];
                                int paramId = obj16.GetParamId(j);
                                if (!this.scripter.MatchAssignment(paramId, num39))
                                {
                                    int num41 = this.symbol_table[num39].TypeId;
                                    ClassObject obj17 = this.GetClassObject(num41);
                                    int num42 = obj17.FindOverloadableImplicitOperatorId(num39, paramId);
                                    if (num42 == 0)
                                    {
                                        flag6 = false;
                                        break;
                                    }
                                    int num43 = this.n;
                                    this.n = pos[j];
                                    int currMethodId = this.GetCurrMethodId();
                                    int num45 = this.AppVar(currMethodId, this.symbol_table[paramId].TypeId);
                                    int res = this[this.n].res;
                                    this[this.n].arg1 = num45;
                                    this.InsertOperators(this.n, 3);
                                    this[this.n].op = this.OP_PUSH;
                                    this[this.n].arg1 = num39;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = num42;
                                    this.n++;
                                    this[this.n].op = this.OP_PUSH;
                                    this[this.n].arg1 = obj17.Id;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = 0;
                                    this.n++;
                                    this[this.n].op = this.OP_CALL_SIMPLE;
                                    this[this.n].arg1 = num42;
                                    this[this.n].arg2 = 1;
                                    this[this.n].res = num45;
                                    this.n = num43 + 3;
                                    for (int k = j + 1; k < a.Count; k++)
                                    {
                                        pos[k] += 3;
                                    }
                                }
                            }
                            if (!flag6)
                            {
                                if ((obj16.ParamsId == 0) || (a.Count == 0))
                                {
                                    this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { str6 });
                                    return;
                                }
                                int num48 = a[a.Count - 1];
                                int num49 = this.symbol_table[num48].TypeId;
                                if (CSLite_System.GetRank(this.symbol_table[num49].Name) != 0)
                                {
                                    this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { str6 });
                                    return;
                                }
                                this.InsertActualArray(obj16, curr_level, pos, a);
                            }
                            id = obj16.Id;
                            n = this.n;
                        }
                    }
                }
                goto Label_17D6;
            }
            int nameIndex = this.symbol_table[id].NameIndex;
            string name = this.symbol_table[id].Name;
            int level = this.symbol_table[id].Level;
            ClassObject classObject = this.GetClassObject(level);
            bool upcase = this.GetUpcase(this.n);
        Label_0646:
            id = classObject.FindMethodId(nameIndex, a, param_mod, 0, out obj10, upcase);
            if (id != 0)
            {
                goto Label_17D6;
            }
            if (obj10 == null)
            {
                this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { classObject.Name, name });
            }
            else if (obj10.ParamCount != a.Count)
            {
                if (a.Count < obj10.ParamCount)
                {
                    int num27;
                    int defaultParamCount = obj10.DefaultParamCount;
                    if ((a.Count + defaultParamCount) < obj10.ParamCount)
                    {
                        if (obj10.ParamsId == 0)
                        {
                            if (((a.Count + 1) == obj10.ParamCount) && (this.GetTypeId(obj10.Param_Ids[obj10.ParamCount - 1]) == 0x10))
                            {
                                int num31;
                                if (pos.Count > 0)
                                {
                                    num31 = pos[a.Count - 1] + 1;
                                }
                                else
                                {
                                    num31 = this.n - 2;
                                }
                                this.InsertOperators(num31, 1);
                                this[num31].op = this.OP_PUSH;
                                this[num31].arg1 = this.symbol_table.NULL_id;
                                this[num31].arg2 = 0;
                                this[num31].res = 0;
                                this.n++;
                                id = obj10.Id;
                                goto Label_0C98;
                            }
                            this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { name, a.Count });
                        }
                        else
                        {
                            if ((a.Count + 1) == obj10.ParamCount)
                            {
                                this.InsertActualArray(obj10, curr_level, pos, a);
                                id = obj10.Id;
                                n = this.n;
                                goto Label_0C98;
                            }
                            this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { name, a.Count });
                        }
                        return;
                    }
                    int number = obj10.ParamCount - a.Count;
                    if (pos.Count > 0)
                    {
                        num27 = pos[a.Count - 1] + 1;
                    }
                    else
                    {
                        num27 = this.n - 2;
                    }
                    ProgRec rec1 = this[this.n];
                    rec1.arg2 += number;
                    this.InsertOperators(num27, number);
                    int num28 = obj10.ParamCount - number;
                    for (int m = 0; m < number; m++)
                    {
                        int num30 = obj10.Default_Ids[num28];
                        a.Add(num30);
                        pos.Add(num27);
                        param_mod.Add(obj10.Param_Mod[num28]);
                        this[num27].op = this.OP_PUSH;
                        this[num27].arg1 = num30;
                        this[num27].arg2 = 0;
                        this[num27].res = obj10.Id;
                        num27++;
                        this.n++;
                        num28++;
                    }
                    this.r = this[this.n];
                    n = this.n;
                    goto Label_0646;
                }
                this.InsertActualArray(obj10, curr_level, pos, a);
                id = obj10.Id;
                n = this.n;
            }
            else
            {
                bool flag3 = true;
                for (int num13 = 0; num13 < a.Count; num13++)
                {
                    int num14 = a[num13];
                    int num15 = obj10.GetParamId(num13);
                    if (!this.scripter.MatchAssignment(num15, num14))
                    {
                        int num16 = this.symbol_table[num14].TypeId;
                        ClassObject obj11 = this.GetClassObject(num16);
                        int num17 = obj11.FindOverloadableImplicitOperatorId(num14, num15);
                        if (num17 == 0)
                        {
                            flag3 = false;
                            break;
                        }
                        int num18 = this.n;
                        this.n = pos[num13];
                        int num19 = this.GetCurrMethodId();
                        int num20 = this.AppVar(num19, this.symbol_table[num15].TypeId);
                        int num21 = this[this.n].res;
                        this[this.n].arg1 = num20;
                        this.InsertOperators(this.n, 3);
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num14;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num17;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = obj11.Id;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL_SIMPLE;
                        this[this.n].arg1 = num17;
                        this[this.n].arg2 = 1;
                        this[this.n].res = num20;
                        this.n = num18 + 3;
                        for (int num22 = num13 + 1; num22 < a.Count; num22++)
                        {
                            pos[num22] += 3;
                        }
                    }
                }
                if (flag3)
                {
                    id = obj10.Id;
                    n = this.n;
                    goto Label_17D6;
                }
                if ((obj10.ParamsId == 0) || (a.Count == 0))
                {
                    this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { name });
                    return;
                }
                int num23 = a[a.Count - 1];
                int num24 = this.symbol_table[num23].TypeId;
                if (CSLite_System.GetRank(this.symbol_table[num24].Name) != 0)
                {
                    this.scripter.CreateErrorObjectEx("CS1502. The best overloaded method match for '{0}' has some invalid arguments.", new object[] { name });
                    return;
                }
                this.InsertActualArray(obj10, curr_level, pos, a);
                id = obj10.Id;
                n = this.n;
            }
        Label_0C98:
            if (id != 0)
            {
                goto Label_17D6;
            }
            return;
        Label_143F:
            if (flag8)
            {
                this[this.n - 1].arg1 = num35;
            }
            else
            {
                this[this.n - 1].arg1 = this.r.arg1;
            }
        Label_17D6:
            this.r.arg1 = id;
            for (avalue = 0; avalue < pos.Count; avalue++)
            {
                this[pos[avalue]].res = id;
            }
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            if (typeId == 1)
            {
                this.r.res = 0;
            }
            else if (this.symbol_table[this.r.arg1].Name != "get_Current")
            {
                this.symbol_table[this.r.res].TypeId = typeId;
            }
            bool flag9 = false;
            string str11 = "";
            string str12 = "";
            FunctionObject functionObject = this.GetFunctionObject(id);
            if (functionObject.Static)
            {
                int num59 = this.symbol_table[id].Level;
                this[n - 1].arg1 = num59;
            }
            if (!functionObject.Imported && (a.Count > 0))
            {
                avalue = 0;
                while (avalue < functionObject.ParamCount)
                {
                    ParamMod paramMod = functionObject.GetParamMod(avalue);
                    ParamMod mod2 = (ParamMod) param_mod[avalue];
                    if (paramMod != mod2)
                    {
                        int num60 = functionObject.GetParamId(avalue);
                        int num61 = this[this.n].arg1;
                        int num62 = this.symbol_table[num60].TypeId;
                        int num63 = this.symbol_table[num61].TypeId;
                        str11 = this.symbol_table[num62].Name;
                        str12 = this.symbol_table[num63].Name;
                        switch (paramMod)
                        {
                            case ParamMod.RetVal:
                                str11 = "ref " + str11;
                                break;

                            case ParamMod.Out:
                                str11 = "out " + str11;
                                break;
                        }
                        if (mod2 == ParamMod.RetVal)
                        {
                            str12 = "ref " + str12;
                        }
                        else if (mod2 == ParamMod.Out)
                        {
                            str12 = "out " + str12;
                        }
                        flag9 = true;
                        if (flag9 && this.PascalOrBasic(this.n))
                        {
                            flag9 = false;
                            if (paramMod == ParamMod.RetVal)
                            {
                                param_mod[avalue] = (int) paramMod;
                                num3++;
                            }
                        }
                        else
                        {
                            this.n = pos[avalue];
                            break;
                        }
                    }
                    avalue++;
                }
            }
            if (flag9)
            {
                this.scripter.CreateErrorObjectEx("CS1503. Argument '{0}': cannot convert from '{1}' to '{2}'.", new object[] { avalue + 1, str12, str11 });
            }
            else if ((num3 > 0) && (a.Count > 0))
            {
                this.InsertOperators(this.n + 1, num3 * 2);
                for (avalue = 0; avalue < functionObject.ParamCount; avalue++)
                {
                    int num64 = functionObject.GetParamId(avalue);
                    if (param_mod[avalue] != 0)
                    {
                        int num65 = this.AppVar(this.symbol_table[id].Level, this.symbol_table[num64].TypeId);
                        this.n++;
                        this[this.n].op = this.OP_GET_PARAM_VALUE;
                        this[this.n].arg1 = id;
                        this[this.n].arg2 = avalue;
                        this[this.n].res = num65;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = a[avalue];
                        this[this.n].arg2 = num65;
                        this[this.n].res = a[avalue];
                    }
                }
            }
        }

        private void CheckOP_CAST()
        {
            bool flag = false;
            int typeId = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(this.r.arg1);
            ClassObject obj3 = this.GetClassObject(typeId);
            if (classObject == obj3)
            {
                flag = true;
                if (classObject.IsEnum)
                {
                    this.r.op = this.OP_TO_ENUM;
                }
            }
            else if (IsNumericTypeId(obj3.Id) && IsNumericTypeId(classObject.Id))
            {
                flag = true;
                int id = classObject.Id;
                switch (id)
                {
                    case 10:
                        this.r.op = this.OP_TO_SBYTE;
                        goto Label_0463;

                    case 3:
                        this.r.op = this.OP_TO_BYTE;
                        goto Label_0463;

                    case 13:
                        this.r.op = this.OP_TO_UINT;
                        goto Label_0463;

                    case 8:
                        this.r.op = this.OP_TO_INT;
                        goto Label_0463;

                    case 15:
                        this.r.op = this.OP_TO_USHORT;
                        goto Label_0463;

                    case 11:
                        this.r.op = this.OP_TO_SHORT;
                        goto Label_0463;

                    case 14:
                        this.r.op = this.OP_TO_ULONG;
                        goto Label_0463;

                    case 9:
                        this.r.op = this.OP_TO_LONG;
                        goto Label_0463;

                    case 4:
                        this.r.op = this.OP_TO_CHAR;
                        goto Label_0463;

                    case 7:
                        this.r.op = this.OP_TO_FLOAT;
                        goto Label_0463;

                    case 5:
                        this.r.op = this.OP_TO_DECIMAL;
                        goto Label_0463;
                }
                if (id == 6)
                {
                    this.r.op = this.OP_TO_DOUBLE;
                }
            }
            else if (obj3.IsEnum && classObject.IsEnum)
            {
                flag = true;
                this.r.op = this.OP_TO_ENUM;
            }
            else if (obj3.IsEnum && IsNumericTypeId(classObject.Id))
            {
                flag = true;
                int num3 = classObject.Id;
                switch (num3)
                {
                    case 10:
                        this.r.op = this.OP_TO_SBYTE;
                        goto Label_0463;

                    case 3:
                        this.r.op = this.OP_TO_BYTE;
                        goto Label_0463;

                    case 13:
                        this.r.op = this.OP_TO_UINT;
                        goto Label_0463;

                    case 8:
                        this.r.op = this.OP_TO_INT;
                        goto Label_0463;

                    case 15:
                        this.r.op = this.OP_TO_USHORT;
                        goto Label_0463;

                    case 11:
                        this.r.op = this.OP_TO_SHORT;
                        goto Label_0463;

                    case 14:
                        this.r.op = this.OP_TO_ULONG;
                        goto Label_0463;

                    case 9:
                        this.r.op = this.OP_TO_LONG;
                        goto Label_0463;

                    case 4:
                        this.r.op = this.OP_TO_CHAR;
                        goto Label_0463;

                    case 7:
                        this.r.op = this.OP_TO_FLOAT;
                        goto Label_0463;

                    case 5:
                        this.r.op = this.OP_TO_DECIMAL;
                        goto Label_0463;
                }
                if (num3 == 6)
                {
                    this.r.op = this.OP_TO_DOUBLE;
                }
            }
            else if (IsNumericTypeId(obj3.Id) && classObject.IsEnum)
            {
                flag = true;
                this.r.op = this.OP_TO_ENUM;
            }
            else if (obj3.Id == this.ObjectTypeId)
            {
                flag = true;
            }
            else if (this.scripter.conversion.ExistsImplicitReferenceConversion(obj3, classObject))
            {
                flag = true;
            }
            else if (obj3.IsClass && classObject.IsClass)
            {
                flag = classObject.InheritsFrom(obj3);
                if (!flag)
                {
                    flag = obj3.InheritsFrom(classObject);
                }
            }
            else if ((obj3.Id == this.symbol_table.DELEGATE_CLASS_id) && classObject.IsDelegate)
            {
                flag = true;
            }
            else if (obj3.IsClass && classObject.IsInterface)
            {
                flag = !obj3.Sealed && !obj3.Implements(classObject);
            }
            else if (obj3.IsInterface && classObject.IsClass)
            {
                flag = !classObject.Sealed || classObject.Implements(obj3);
            }
            else if (obj3.IsInterface && classObject.IsInterface)
            {
                flag = !classObject.InheritsFrom(obj3);
            }
            else if (classObject.Id == 0x10)
            {
                flag = true;
            }
        Label_0463:
            if (!flag)
            {
                flag = classObject.InheritsFrom(obj3);
            }
            this.symbol_table[this.r.res].TypeId = classObject.Id;
            if (!flag)
            {
                ClassObject obj4;
                int num4 = obj3.FindOverloadableExplicitOperatorId(this.r.res);
                if (num4 == 0)
                {
                    num4 = classObject.FindOverloadableExplicitOperatorId(this.r.res);
                    obj4 = classObject;
                }
                else
                {
                    obj4 = obj3;
                }
                if (num4 == 0)
                {
                    this.scripter.CreateErrorObjectEx("CS0030. Cannot convert type '{0}' to '{1}'.", new object[] { obj3.Name, classObject.Name });
                }
                else
                {
                    this.InsertOperators(this.n, 2);
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = this.r.arg2;
                    this[this.n].arg2 = 0;
                    this[this.n].res = num4;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = obj4.Id;
                    this[this.n].arg2 = 0;
                    this[this.n].res = 0;
                    this.n++;
                    this[this.n].op = this.OP_CALL_SIMPLE;
                    this[this.n].arg1 = num4;
                    this[this.n].arg2 = 1;
                    this[this.n].res = this.r.res;
                }
            }
        }

        private void CheckOP_CREATE_REFERENCE(ClassObject current_class)
        {
            MemberObject memberByNameIndex;
            int num35;
            if (this.symbol_table[this.r.arg1].Kind != MemberKind.Type)
            {
                EventObject obj9;
                int num10;
                this.scripter.Dump();
                int typeId = this.symbol_table[this.r.arg1].TypeId;
                ClassObject classObjectEx = this.GetClassObjectEx(typeId);
                int nameIndex = this.symbol_table[this.r.res].NameIndex;
                string name = this.symbol_table[this.r.res].Name;
                bool upcase = this.GetUpcase(this.n);
                memberByNameIndex = classObjectEx.GetMemberByNameIndex(nameIndex, upcase);
                if (((memberByNameIndex == null) && upcase) && ((name.ToUpper() == "NEW") && this.PascalOrBasic(this.n)))
                {
                    FunctionObject obj4;
                    IntegerList a = new IntegerList(false);
                    IntegerList list2 = new IntegerList(false);
                    classObjectEx.FindConstructorId(a, list2, out obj4);
                    if (obj4 != null)
                    {
                        memberByNameIndex = obj4;
                    }
                }
                if (memberByNameIndex == null)
                {
                    this.scripter.CreateErrorObjectEx("CS0103. The name '{0}' does not exist in the class or namespace '{1}'.", new object[] { name, classObjectEx.Name });
                    return;
                }
                if (memberByNameIndex.Static)
                {
                    memberByNameIndex = classObjectEx.GetInstanceMemberByNameIndex(nameIndex, upcase);
                    if (memberByNameIndex == null)
                    {
                        this.scripter.CreateErrorObjectEx("CS0176. Static member '{0}' cannot be accessed with an instance reference; qualify it with a type name instead.", new object[] { name });
                        return;
                    }
                }
                if (memberByNameIndex.Private)
                {
                    if (current_class == null)
                    {
                        this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                        return;
                    }
                    if (classObjectEx.Id != current_class.Id)
                    {
                        this.scripter.CreateErrorObjectEx("CS0122. '{0}' is inaccessible due to its protection level.", new object[] { memberByNameIndex.Name });
                        return;
                    }
                }
                if (memberByNameIndex.Protected)
                {
                    if (current_class == null)
                    {
                        this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                        return;
                    }
                    if ((classObjectEx.Id != current_class.Id) && !current_class.InheritsFrom(classObjectEx))
                    {
                        this.scripter.CreateErrorObjectEx("CS0122. '{0}' is inaccessible due to its protection level.", new object[] { memberByNameIndex.Name });
                        return;
                    }
                }
                if (memberByNameIndex.Kind == MemberKind.Constructor)
                {
                    this.ReplaceId(this.r.res, memberByNameIndex.Id);
                    this.r.op = this.OP_NOP;
                    goto Label_17DB;
                }
                if (memberByNameIndex.Kind != MemberKind.Method)
                {
                    if (memberByNameIndex.Kind != MemberKind.Event)
                    {
                        if (memberByNameIndex.Kind != MemberKind.Property)
                        {
                            goto Label_17DB;
                        }
                        PropertyObject obj10 = (PropertyObject) memberByNameIndex;
                        int num18 = 0;
                        bool flag4 = false;
                        bool flag5 = false;
                        for (int j = this.n; j <= this.Card; j++)
                        {
                            if (this[j].arg1 == this.r.res)
                            {
                                if (this[j].op == this.OP_ASSIGN)
                                {
                                    num18 = j;
                                    break;
                                }
                                if ((this[j].op == this.OP_CALL) && this.PascalOrBasic(this.n))
                                {
                                    if ((this[j + 1].op != this.OP_ASSIGN) || (this[j + 1].arg1 != this[j].res))
                                    {
                                        continue;
                                    }
                                    num18 = j + 1;
                                    flag4 = true;
                                    break;
                                }
                                flag5 = true;
                            }
                        }
                        if (num18 > 0)
                        {
                            if (obj10.WriteId == 0)
                            {
                                this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj10.Name });
                                return;
                            }
                            FunctionObject obj11 = this.GetFunctionObject(obj10.WriteId);
                            if (flag4)
                            {
                                int num20 = this[num18].arg2;
                                this.ReplaceId(this.r.res, obj10.WriteId);
                                this.r.op = this.OP_NOP;
                                this[num18 - 2].arg1 = this.r.arg1;
                                this[num18].op = this.OP_NOP;
                                ProgRec rec1 = this[num18 - 1];
                                rec1.arg2++;
                                if (obj11.ParamCount != this[num18 - 1].arg2)
                                {
                                }
                                this.InsertOperators(num18 - 2, 1);
                                this[num18 - 2].op = this.OP_PUSH;
                                this[num18 - 2].arg1 = num20;
                                this[num18 - 2].arg2 = 0;
                                this[num18 - 2].res = obj10.WriteId;
                                return;
                            }
                            this[num18].op = this.OP_PUSH;
                            this[num18].arg1 = this[num18].arg2;
                            this[num18].arg2 = 0;
                            this[num18].res = obj10.WriteId;
                            this[num18 + 1].op = this.OP_PUSH;
                            this[num18 + 1].arg1 = this.r.arg1;
                            this[num18 + 1].arg2 = 0;
                            this[num18 + 1].res = 0;
                            this[num18 + 2].op = this.OP_CALL_SIMPLE;
                            this[num18 + 2].arg1 = obj10.WriteId;
                            this[num18 + 2].arg2 = 1;
                            this[num18 + 2].res = 0;
                            if (flag5)
                            {
                                if (obj10.ReadId == 0)
                                {
                                    this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj10.Name });
                                    return;
                                }
                                this[this.n - 1].op = this.OP_PUSH;
                                this[this.n - 1].arg1 = this.r.arg1;
                                this[this.n - 1].arg2 = 0;
                                this[this.n - 1].res = 0;
                                this[this.n].op = this.OP_CALL_SIMPLE;
                                this[this.n].arg1 = obj10.ReadId;
                                this[this.n].arg2 = 0;
                                this.symbol_table[this[this.n].res].Kind = MemberKind.Var;
                                this.symbol_table[this[this.n].res].TypeId = this.symbol_table[obj10.ReadId].TypeId;
                            }
                            else
                            {
                                this.r.op = this.OP_NOP;
                            }
                            if (obj11.ParamCount != 1)
                            {
                                this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { obj11.FullName, 1 });
                            }
                            goto Label_17DB;
                        }
                        if (obj10.ReadId == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj10.Name });
                            return;
                        }
                        FunctionObject functionObject = this.GetFunctionObject(obj10.ReadId);
                        int n = this.n;
                        bool flag6 = false;
                    Label_0E61:
                        n++;
                        if (n != this.card)
                        {
                            if ((this[n].op != this.OP_BEGIN_CALL) || (this[n].arg1 != this.r.res))
                            {
                                goto Label_0E61;
                            }
                            flag6 = true;
                        }
                        if (!flag6 || !this.PascalOrBasic(this.n))
                        {
                            this[this.n - 1].op = this.OP_PUSH;
                            this[this.n - 1].arg1 = this.r.arg1;
                            this.r.op = this.OP_CALL_SIMPLE;
                            this.r.arg1 = obj10.ReadId;
                            this.r.arg2 = 0;
                            this.symbol_table[this.r.res].Kind = MemberKind.Var;
                            this.symbol_table[this.r.res].TypeId = this.symbol_table[obj10.ReadId].TypeId;
                            if (functionObject.ParamCount != 0)
                            {
                                this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, 0 });
                            }
                            goto Label_17DB;
                        }
                        this.ReplaceId(this.r.res, obj10.ReadId);
                        this.r.op = this.OP_NOP;
                        IntegerList list3 = new IntegerList(true);
                        IntegerList list4 = new IntegerList(true);
                        int pos = 0;
                        int avalue = n;
                        do
                        {
                            avalue++;
                            if ((this[avalue].op == this.OP_PUSH) && (this[avalue].res == obj10.ReadId))
                            {
                                list3.Add(avalue);
                                list4.Add(this[avalue].arg1);
                            }
                        }
                        while ((this[avalue].op != this.OP_CALL) || (this[avalue].arg1 != obj10.ReadId));
                        this[avalue].tag = avalue;
                        this.get_item_list.Add(this[avalue]);
                        pos = avalue;
                        this[avalue - 1].arg1 = this.r.arg1;
                        if (functionObject.ParamCount != this[avalue].arg2)
                        {
                            if (functionObject.Owner.HasMethod(functionObject.NameIndex, this[avalue].arg2))
                            {
                                return;
                            }
                            if (functionObject.ParamCount == 0)
                            {
                                this[avalue].arg2 = 0;
                                for (int k = 0; k < list3.Count; k++)
                                {
                                    this[list3[k]].op = this.OP_NOP;
                                }
                                int resultId = functionObject.ResultId;
                                int id = this.symbol_table[resultId].TypeId;
                                ClassObject classObject = this.GetClassObject(id);
                                int num27 = this.scripter.names.Add("get_Item");
                                MemberObject obj14 = classObject.GetMemberByNameIndex(num27, true);
                                if (obj14 == null)
                                {
                                    this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this[avalue].arg2 });
                                }
                                else
                                {
                                    int res = this[avalue].res;
                                    int num29 = this.AppVar(this.symbol_table[res].Level);
                                    this.symbol_table[num29].TypeId = id;
                                    this[avalue].res = num29;
                                    int num30 = obj14.Id;
                                    this.InsertOperators(avalue + 1, list4.Count + 3);
                                    avalue++;
                                    this[avalue].op = this.OP_BEGIN_CALL;
                                    this[avalue].arg1 = num30;
                                    this[avalue].arg2 = 0;
                                    this[avalue].res = 0;
                                    for (int m = 0; m < list3.Count; m++)
                                    {
                                        avalue++;
                                        this[avalue].op = this.OP_PUSH;
                                        this[avalue].arg1 = list4[m];
                                        this[avalue].arg2 = 0;
                                        this[avalue].res = num30;
                                    }
                                    avalue++;
                                    this[avalue].op = this.OP_PUSH;
                                    this[avalue].arg1 = num29;
                                    this[avalue].arg2 = 0;
                                    this[avalue].res = 0;
                                    avalue++;
                                    this[avalue].op = this.OP_CALL;
                                    this[avalue].arg1 = num30;
                                    this[avalue].arg2 = list4.Count;
                                    this[avalue].res = res;
                                    resultId = this.GetFunctionObject(num30).ResultId;
                                    id = this.symbol_table[resultId].TypeId;
                                    this.symbol_table[res].TypeId = id;
                                    this.n = avalue - 1;
                                }
                                return;
                            }
                            this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this[avalue].arg2 });
                        }
                        if (this.get_item_list.Count < 2)
                        {
                            return;
                        }
                        ProgRec rec = this.get_item_list[this.get_item_list.Count - 2] as ProgRec;
                        ProgRec rec2 = this.get_item_list[this.get_item_list.Count - 1] as ProgRec;
                        if (rec.arg1 != rec2.arg1)
                        {
                            return;
                        }
                        while ((this[pos + 1].op != this.OP_ASSIGN) || (this[pos + 1].arg1 != rec.res))
                        {
                            pos++;
                            if (pos == this.card)
                            {
                                return;
                            }
                        }
                        if ((this[pos + 1].op != this.OP_ASSIGN) || (this[pos + 1].arg1 != rec.res))
                        {
                            return;
                        }
                        if (obj10.WriteId == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj10.Name });
                            return;
                        }
                        int num32 = this[pos + 1].arg2;
                        this[pos + 1].op = this.OP_NOP;
                        int tag = rec.tag;
                        ArrayList list5 = new ArrayList();
                        list5.Insert(0, this[tag].Clone());
                        this[tag].op = this.OP_NOP;
                        tag--;
                        list5.Insert(0, this[tag].Clone());
                        this[tag].op = this.OP_NOP;
                        int num34 = 0;
                        while (true)
                        {
                            tag--;
                            if ((this[tag].op == this.OP_PUSH) && (this[tag].res == rec.arg1))
                            {
                                list5.Insert(0, this[tag].Clone());
                                this[tag].op = this.OP_NOP;
                                num34++;
                                if (num34 == rec.arg2)
                                {
                                    pos++;
                                    this.InsertOperators(pos, list5.Count);
                                    this[pos].op = this.OP_BEGIN_CALL;
                                    this[pos].arg1 = obj10.WriteId;
                                    this[pos].arg2 = 0;
                                    this[pos].res = 0;
                                    for (num34 = 0; num34 <= (list5.Count - 3); num34++)
                                    {
                                        pos++;
                                        this[pos].op = this.OP_PUSH;
                                        this[pos].arg1 = (list5[num34] as ProgRec).arg1;
                                        this[pos].arg2 = (list5[num34] as ProgRec).arg2;
                                        this[pos].res = obj10.WriteId;
                                    }
                                    pos++;
                                    this[pos].op = this.OP_PUSH;
                                    this[pos].arg1 = num32;
                                    this[pos].arg2 = 0;
                                    this[pos].res = obj10.WriteId;
                                    pos++;
                                    this[pos].op = this.OP_PUSH;
                                    this[pos].arg1 = (list5[list5.Count - 2] as ProgRec).arg1;
                                    this[pos].arg2 = 0;
                                    this[pos].res = 0;
                                    pos++;
                                    this[pos].op = this.OP_CALL;
                                    this[pos].arg1 = obj10.WriteId;
                                    this[pos].arg2 = rec.arg2 + 1;
                                    this[pos].res = 0;
                                    return;
                                }
                            }
                        }
                    }
                    obj9 = (EventObject) memberByNameIndex;
                    num10 = 0;
                    for (int i = this.n; i <= this.Card; i++)
                    {
                        if ((this[i].arg1 == this.r.res) && (this[i].op == this.OP_ASSIGN))
                        {
                            num10 = i;
                            break;
                        }
                    }
                }
                else
                {
                    int num3 = this.n - 1;
                    while ((this[num3].op == this.OP_NOP) || (this[num3].op == this.OP_SEPARATOR))
                    {
                        num3--;
                    }
                    bool isDelegate = false;
                    ClassObject obj5 = null;
                    if (this[num3].op == this.OP_BEGIN_CALL)
                    {
                        int num4 = this[num3].arg1;
                        if (this.symbol_table[num4].Kind == MemberKind.Type)
                        {
                            obj5 = this.GetClassObject(num4);
                            isDelegate = obj5.IsDelegate;
                        }
                    }
                    if (isDelegate)
                    {
                        FunctionObject obj6;
                        FunctionObject patternMethod = obj5.PatternMethod;
                        if (patternMethod.Init == null)
                        {
                            FunctionObject g = this.GetFunctionObject(memberByNameIndex.Id);
                            this.CreatePatternMethod(patternMethod, g);
                        }
                        int num5 = classObjectEx.FindMethodId(nameIndex, patternMethod.Param_Ids, patternMethod.Param_Mod, patternMethod.ResultId, out obj6, upcase);
                        if (num5 == 0)
                        {
                            if (classObjectEx.GetMemberByNameIndex(nameIndex, upcase) != null)
                            {
                                this.scripter.CreateErrorObject("CS0149. Method name expected.");
                            }
                            else
                            {
                                string str2 = obj5.Name;
                                this.scripter.CreateErrorObjectEx("CS0123. Method '{0}' does not match delegate '{1}'.", new object[] { name, str2 });
                            }
                            return;
                        }
                        this.ReplaceId(this.r.res, num5);
                        this.r.res = obj5.Id;
                        this.r.op = this.OP_PUSH;
                    }
                    else if (!this.PascalOrBasic(this.n))
                    {
                        this.r.op = this.OP_NOP;
                    }
                    else
                    {
                        int num6 = this.r.arg1;
                        int num7 = memberByNameIndex.Id;
                        if (this[this.n + 1].op == this.OP_ADDRESS_OF)
                        {
                            this.r.op = this.OP_NOP;
                            this[this.n + 1].arg1 = num7;
                        }
                        else
                        {
                            bool flag3 = false;
                            for (int num8 = this.n; num8 < this.card; num8++)
                            {
                                int op = this[num8].op;
                                if ((this[num8].arg1 == this.r.res) && (((op == this.OP_CALL) || (op == this.OP_CALL_BASE)) || ((op == this.OP_CALL_VIRT) || (op == this.OP_CALL_SIMPLE))))
                                {
                                    flag3 = true;
                                    break;
                                }
                            }
                            if (flag3)
                            {
                                this.r.op = this.OP_NOP;
                            }
                            else
                            {
                                this.InsertOperators(this.n, 2);
                                this[this.n].op = this.OP_BEGIN_CALL;
                                this[this.n].arg1 = num7;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = num6;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.r.op = this.OP_CALL;
                                this.r.arg1 = num7;
                                this.r.arg2 = 0;
                                this.symbol_table[this.r.res].Kind = MemberKind.Var;
                            }
                        }
                    }
                    goto Label_17DB;
                }
                if (num10 == 0)
                {
                    if (obj9.EventFieldId == 0)
                    {
                        this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj9.Name });
                        return;
                    }
                    string str3 = this.symbol_table[obj9.EventFieldId].Name;
                    this.symbol_table[this.r.res].Name = str3;
                    goto Label_17DB;
                }
                num10--;
                if (this[num10].op == this.OP_PLUS)
                {
                    if (obj9.AddId == 0)
                    {
                        this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj9.Name });
                        return;
                    }
                    int addId = obj9.AddId;
                    int num13 = this[num10].arg2;
                    int num14 = this.r.arg1;
                    this.r.op = this.OP_NOP;
                    this[num10].op = this.OP_PUSH;
                    this[num10].arg1 = num13;
                    this[num10].arg2 = 0;
                    this[num10].res = addId;
                    num10++;
                    this[num10].op = this.OP_PUSH;
                    this[num10].arg1 = num14;
                    this[num10].arg2 = 0;
                    this[num10].res = 0;
                    num10++;
                    this[num10].op = this.OP_CALL_SIMPLE;
                    this[num10].arg1 = addId;
                    this[num10].arg2 = 1;
                    this[num10].res = 0;
                    goto Label_17DB;
                }
                if (this[num10].op == this.OP_MINUS)
                {
                    if (obj9.RemoveId == 0)
                    {
                        this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { obj9.Name });
                        return;
                    }
                    int removeId = obj9.RemoveId;
                    int num16 = this[num10].arg2;
                    int num17 = this.r.arg1;
                    this.r.op = this.OP_NOP;
                    this[num10].op = this.OP_PUSH;
                    this[num10].arg1 = num16;
                    this[num10].arg2 = 0;
                    this[num10].res = removeId;
                    num10++;
                    this[num10].op = this.OP_PUSH;
                    this[num10].arg1 = num17;
                    this[num10].arg2 = 0;
                    this[num10].res = 0;
                    num10++;
                    this[num10].op = this.OP_CALL_SIMPLE;
                    this[num10].arg1 = removeId;
                    this[num10].arg2 = 1;
                    this[num10].res = 0;
                    goto Label_17DB;
                }
                this.scripter.CreateErrorObjectEx("CS0079. The event '{0}' can only appear on the left hand side of += or -=.", new object[] { obj9.Name });
            }
            return;
        Label_17DB:
            num35 = this.symbol_table[memberByNameIndex.Id].TypeId;
            this.symbol_table[this.r.res].TypeId = num35;
        }

        private void CheckOP_DEC()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            if (!this.TryOverloadableUnaryOperator("op_Decrement", classObject))
            {
                if (IsNumericTypeId(classObject.Id))
                {
                    if (this.SetupDetailedUnaryOperator(op, "--", this.detailed_dec_operators))
                    {
                        this.symbol_table[this.r.res].TypeId = typeId;
                    }
                }
                else if (classObject.Class_Kind == ClassKind.Enum)
                {
                    if (!this.TryDetailedUnaryOperator(this.detailed_dec_operators, classObject.UnderlyingType))
                    {
                        string name = this.symbol_table[this.r.arg1].Name;
                        this.scripter.CreateErrorObjectEx("CS0023. Operator '{0}' cannot be applied to operand of type '{1}'.", new object[] { "--", name });
                    }
                    else
                    {
                        this.symbol_table[this.r.res].TypeId = typeId;
                    }
                }
                else
                {
                    string str2 = this.symbol_table[this.r.arg1].Name;
                    this.scripter.CreateErrorObjectEx("CS0023. Operator '{0}' cannot be applied to operand of type '{1}'.", new object[] { "--", str2 });
                }
            }
        }

        private void CheckOP_DIV()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "/", this.detailed_division_operators))
            {
                if (this.symbol_table[this.r.res].TypeId == 0)
                {
                    this.symbol_table[this.r.res].TypeId = this.symbol_table[this.r.arg1].TypeId;
                }
                if (this[this.n].op != this.OP_CALL_SIMPLE)
                {
                    this.r = this[this.n];
                    int typeId = this.GetTypeId(this.r.arg1);
                    int num3 = this.GetTypeId(this.r.arg2);
                    int id = this.GetTypeId(this.r.res);
                    if ((typeId != id) && IsNumericTypeId(id))
                    {
                        this.InsertNumericConversion(id, 1);
                    }
                    if ((num3 != id) && IsNumericTypeId(id))
                    {
                        this.InsertNumericConversion(id, 2);
                    }
                }
            }
        }

        private void CheckOP_EQ()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ClassObject obj3 = this.GetClassObject(id);
            if (classObject.IsDelegate)
            {
                if ((typeId == id) || (id == this.symbol_table.OBJECT_CLASS_id))
                {
                    this.r.op = this.OP_EQ_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = 2;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "==", classObject.Name, obj3.Name });
                }
            }
            else if (obj3.IsDelegate)
            {
                if ((typeId == id) || (typeId == this.symbol_table.OBJECT_CLASS_id))
                {
                    this.r.op = this.OP_EQ_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = 2;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "==", classObject.Name, obj3.Name });
                }
            }
            else
            {
                if ((typeId == 12) && (this.r.arg2 == this.symbol_table.NULL_id))
                {
                    this.r.op = this.OP_EQ_STRING;
                }
                else if ((id == 12) && (this.r.arg1 == this.symbol_table.NULL_id))
                {
                    this.r.op = this.OP_EQ_STRING;
                }
                else if (this.r.arg1 == this.symbol_table.NULL_id)
                {
                    if (obj3.IsValueType)
                    {
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "==", "<null>", obj3.Name });
                    }
                }
                else if (this.r.arg2 == this.symbol_table.NULL_id)
                {
                    if (classObject.IsValueType)
                    {
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "==", classObject.Name, "<null>" });
                    }
                }
                else if (!this.SetupDetailedBinaryOperator(op, "==", this.detailed_eq_operators))
                {
                }
                this.symbol_table[this.r.res].TypeId = 2;
            }
        }

        private void CheckOP_EXP()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "^", this.detailed_exponent_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != 8) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(8, 2);
                }
            }
        }

        private void CheckOP_INC()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            if (!this.TryOverloadableUnaryOperator("op_Increment", classObject))
            {
                if (IsNumericTypeId(classObject.Id))
                {
                    if (this.SetupDetailedUnaryOperator(op, "++", this.detailed_inc_operators))
                    {
                        this.symbol_table[this.r.res].TypeId = typeId;
                    }
                }
                else if (classObject.Class_Kind == ClassKind.Enum)
                {
                    if (!this.TryDetailedUnaryOperator(this.detailed_inc_operators, classObject.UnderlyingType))
                    {
                        string name = this.symbol_table[this.r.arg1].Name;
                        this.scripter.CreateErrorObjectEx("CS0023. Operator '{0}' cannot be applied to operand of type '{1}'.", new object[] { "++", name });
                    }
                    else
                    {
                        this.symbol_table[this.r.res].TypeId = typeId;
                    }
                }
                else
                {
                    string str2 = this.symbol_table[this.r.arg1].Name;
                    this.scripter.CreateErrorObjectEx("CS0023. Operator '{0}' cannot be applied to operand of type '{1}'.", new object[] { "++", str2 });
                }
            }
        }

        private void CheckOP_LEFT_SHIFT()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "<<", this.detailed_left_shift_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_LOGICAL_AND()
        {
            int card = this.symbol_table.Card;
            if (this.SetupDetailedBinaryOperator(this.OP_BITWISE_AND, "&&", this.detailed_bitwise_and_operators))
            {
                int num2 = this.symbol_table.Card;
                if (card == num2)
                {
                    int typeId = this.symbol_table[this.r.arg1].TypeId;
                    int num4 = this.symbol_table[this.r.arg2].TypeId;
                    if ((typeId == 2) && (num4 == 2))
                    {
                        int num5 = this.r.arg1;
                        int num6 = this.r.arg2;
                        int res = this.r.res;
                        this.r.op = this.OP_NOP;
                        int num8 = this.symbol_table.AppLabel();
                        int num9 = this.symbol_table.AppLabel();
                        this.n++;
                        this.InsertOperators(this.n, 4);
                        this[this.n].op = this.OP_GO_FALSE;
                        this[this.n].arg1 = num8;
                        this[this.n].arg2 = num5;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = res;
                        this[this.n].arg2 = num6;
                        this[this.n].res = res;
                        this.n++;
                        this[this.n].op = this.OP_GO;
                        this[this.n].arg1 = num9;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = res;
                        this[this.n].arg2 = this.symbol_table.FALSE_id;
                        this[this.n].res = res;
                        this.symbol_table.SetLabel(num8, this.n);
                        this.n++;
                        this.symbol_table.SetLabel(num9, this.n);
                    }
                    else
                    {
                        string name = this.symbol_table[this.r.arg1].Name;
                        string str2 = this.symbol_table[this.r.arg2].Name;
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "&&", name, str2 });
                    }
                }
            }
        }

        private void CheckOP_LOGICAL_OR()
        {
            int card = this.symbol_table.Card;
            if (this.SetupDetailedBinaryOperator(this.OP_BITWISE_OR, "||", this.detailed_bitwise_or_operators))
            {
                int num2 = this.symbol_table.Card;
                if (card == num2)
                {
                    int typeId = this.symbol_table[this.r.arg1].TypeId;
                    int num4 = this.symbol_table[this.r.arg2].TypeId;
                    if ((typeId == 2) && (num4 == 2))
                    {
                        int num5 = this.r.arg1;
                        int num6 = this.r.arg2;
                        int res = this.r.res;
                        this.r.op = this.OP_NOP;
                        int num8 = this.symbol_table.AppLabel();
                        int num9 = this.symbol_table.AppLabel();
                        this.n++;
                        this.InsertOperators(this.n, 4);
                        this[this.n].op = this.OP_GO_TRUE;
                        this[this.n].arg1 = num8;
                        this[this.n].arg2 = num5;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = res;
                        this[this.n].arg2 = num6;
                        this[this.n].res = res;
                        this.n++;
                        this[this.n].op = this.OP_GO;
                        this[this.n].arg1 = num9;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = res;
                        this[this.n].arg2 = this.symbol_table.TRUE_id;
                        this[this.n].res = res;
                        this.symbol_table.SetLabel(num8, this.n);
                        this.n++;
                        this.symbol_table.SetLabel(num9, this.n);
                    }
                    else
                    {
                        string name = this.symbol_table[this.r.arg1].Name;
                        string str2 = this.symbol_table[this.r.arg2].Name;
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "||", name, str2 });
                    }
                }
            }
        }

        private void CheckOP_MINUS()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ClassObject obj3 = this.GetClassObject(id);
            if (classObject.IsDelegate)
            {
                if (typeId == id)
                {
                    this.r.op = this.OP_SUB_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = typeId;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "-", classObject.Name, obj3.Name });
                }
            }
            else if (this.SetupDetailedBinaryOperator(op, "-", this.detailed_subtraction_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                typeId = this.GetTypeId(this.r.arg1);
                id = this.GetTypeId(this.r.arg2);
                int num4 = this.GetTypeId(this.r.res);
                if ((typeId != num4) && IsNumericTypeId(num4))
                {
                    this.InsertNumericConversion(num4, 1);
                }
                if ((id != num4) && IsNumericTypeId(num4))
                {
                    this.InsertNumericConversion(num4, 2);
                }
            }
        }

        private void CheckOP_MOD()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "%", this.detailed_remainder_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_MULT()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, "*", this.detailed_multiplication_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        private void CheckOP_NE()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ClassObject obj3 = this.GetClassObject(id);
            if (classObject.IsDelegate)
            {
                if ((typeId == id) || (id == this.symbol_table.OBJECT_CLASS_id))
                {
                    this.r.op = this.OP_NE_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = 2;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "!=", classObject.Name, obj3.Name });
                }
            }
            else if (obj3.IsDelegate)
            {
                if ((typeId == id) || (typeId == this.symbol_table.OBJECT_CLASS_id))
                {
                    this.r.op = this.OP_NE_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = 2;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "!=", classObject.Name, obj3.Name });
                }
            }
            else
            {
                if ((typeId == 12) && (this.r.arg2 == this.symbol_table.NULL_id))
                {
                    this.r.op = this.OP_NE_STRING;
                }
                else if ((id == 12) && (this.r.arg1 == this.symbol_table.NULL_id))
                {
                    this.r.op = this.OP_NE_STRING;
                }
                else if (this.r.arg1 == this.symbol_table.NULL_id)
                {
                    if (obj3.IsValueType)
                    {
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "!=", "<null>", obj3.Name });
                    }
                }
                else if (this.r.arg2 == this.symbol_table.NULL_id)
                {
                    if (classObject.IsValueType)
                    {
                        this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "!=", classObject.Name, "<null>" });
                    }
                }
                else if (!this.SetupDetailedBinaryOperator(op, "!=", this.detailed_ne_operators))
                {
                }
                this.symbol_table[this.r.res].TypeId = 2;
            }
        }

        private void CheckOP_PLUS()
        {
            int op = this.r.op;
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ClassObject obj3 = this.GetClassObject(id);
            if (classObject.IsDelegate)
            {
                if (typeId == id)
                {
                    this.r.op = this.OP_ADD_DELEGATES;
                    this.symbol_table[this.r.res].TypeId = typeId;
                }
                else
                {
                    this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { "+", classObject.Name, obj3.Name });
                }
            }
            else if (this.SetupDetailedBinaryOperator(op, "+", this.detailed_addition_operators))
            {
                if (this.r.op == this.OP_ADDITION_STRING)
                {
                    if (classObject.Id != 12)
                    {
                        FunctionObject obj4;
                        int num4 = this.AppVar(this.symbol_table[this.r.arg1].Level, 12);
                        bool upcase = this.GetUpcase();
                        int num5 = classObject.FindMethodId("ToString", null, null, 0, out obj4, upcase);
                        if (num5 == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { classObject.Name, "ToString" });
                            return;
                        }
                        this.InsertOperators(this.n, 2);
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = this.r.arg1;
                        this[this.n].arg2 = 0;
                        this[this.n].arg2 = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL;
                        this[this.n].arg1 = num5;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num4;
                        this.r.arg1 = num4;
                    }
                    if (obj3.Id != 12)
                    {
                        FunctionObject obj5;
                        int num6 = this.AppVar(this.symbol_table[this.r.arg2].Level, 12);
                        bool flag2 = this.GetUpcase();
                        int num7 = obj3.FindMethodId("ToString", null, null, 0, out obj5, flag2);
                        if (num7 == 0)
                        {
                            if (obj3.IsEnum)
                            {
                                num7 = obj3.UnderlyingType.FindMethodId("ToString", null, null, 0, out obj5, flag2);
                            }
                            if (num7 == 0)
                            {
                                this.scripter.CreateErrorObjectEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { obj3.Name, "ToString" });
                                return;
                            }
                        }
                        this.InsertOperators(this.n, 2);
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = this.r.arg2;
                        this[this.n].arg2 = 0;
                        this[this.n].arg2 = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL;
                        this[this.n].arg1 = num7;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num6;
                        this.r.arg2 = num6;
                    }
                }
                else if (this[this.n].op != this.OP_CALL_SIMPLE)
                {
                    this.r = this[this.n];
                    typeId = this.GetTypeId(this.r.arg1);
                    id = this.GetTypeId(this.r.arg2);
                    int num8 = this.GetTypeId(this.r.res);
                    if ((typeId != num8) && IsNumericTypeId(num8))
                    {
                        this.InsertNumericConversion(num8, 1);
                    }
                    if ((id != num8) && IsNumericTypeId(num8))
                    {
                        this.InsertNumericConversion(num8, 2);
                    }
                }
            }
        }

        private void CheckOP_RIGHT_SHIFT()
        {
            int op = this.r.op;
            if (this.SetupDetailedBinaryOperator(op, ">>", this.detailed_right_shift_operators) && (this[this.n].op != this.OP_CALL_SIMPLE))
            {
                this.r = this[this.n];
                int typeId = this.GetTypeId(this.r.arg1);
                int num3 = this.GetTypeId(this.r.arg2);
                int id = this.GetTypeId(this.r.res);
                if ((typeId != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 1);
                }
                if ((num3 != id) && IsNumericTypeId(id))
                {
                    this.InsertNumericConversion(id, 2);
                }
            }
        }

        public void CheckTypes()
        {
            this.CheckTypesEx(0, 0, null);
        }

        public void CheckTypesEx(int init_n, int init_level, IntegerStack init_class_stack)
        {
            ClassObject classObject;
            IntegerStack stack;
            FunctionObject obj9;
            int num52;
            if (this.scripter.IsError())
            {
                return;
            }
            IntegerList a = new IntegerList(true);
            IntegerList list2 = new IntegerList(true);
            IntegerList pos = new IntegerList(true);
            IntegerList list4 = new IntegerList(false);
            IntegerList l = new IntegerList(false);
            ClassObject obj2 = null;
            if (init_class_stack == null)
            {
                stack = new IntegerStack();
            }
            else
            {
                stack = init_class_stack.Clone();
            }
            if (stack.Count == 0)
            {
                classObject = null;
            }
            else
            {
                classObject = this.GetClassObject(stack.Peek());
            }
            this.n = init_n;
            int num = init_level;
        Label_0079:
            this.n++;
            if (this.n >= this.Card)
            {
                goto Label_22C6;
            }
            this.r = (ProgRec) this.prog[this.n];
            int op = this.r.op;
            if ((op == this.OP_SEPARATOR) || (op == this.OP_LABEL))
            {
                goto Label_0079;
            }
            if (op != this.OP_CREATE_INDEX_OBJECT)
            {
                if (op == this.OP_ADD_INDEX)
                {
                    if (((this.symbol_table[this.r.res].TypeId != 12) || (this[this.n + 1].op != this.OP_ADD_INDEX)) || (this[this.n + 1].res != this.r.res))
                    {
                        goto Label_04A6;
                    }
                    int num7 = 1;
                    while ((this[this.n + num7].op == this.OP_ADD_INDEX) && (this[this.n + num7].res == this.r.res))
                    {
                        num7++;
                    }
                    this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { "this", num7 });
                    goto Label_22C6;
                }
                if (op == this.OP_SETUP_INDEX_OBJECT)
                {
                    int n = this.n;
                    int num9 = 0;
                    do
                    {
                        n--;
                        if ((this[n].op == this.OP_ADD_INDEX) && (this[n].arg1 == this.r.arg1))
                        {
                            num9++;
                        }
                    }
                    while ((this[n].op != this.OP_CREATE_INDEX_OBJECT) || (this[n].res != this.r.arg1));
                    int typeId = this.symbol_table[this[n].arg1].TypeId;
                    int rank = CSLite_System.GetRank(this.symbol_table[typeId].Name);
                    if ((rank > 0) && (rank != num9))
                    {
                        this.scripter.CreateErrorObjectEx("CS0022. Wrong number of indices inside [], expected '{0}'.", new object[] { rank.ToString() });
                        goto Label_22C6;
                    }
                }
            }
            else
            {
                int num3 = this.symbol_table[this.r.arg1].TypeId;
                string name = this.symbol_table[num3].Name;
                string elementTypeName = CSLite_System.GetElementTypeName(name);
                int num4 = 0;
                PropertyObject obj4 = null;
                if (elementTypeName == "")
                {
                    ClassObject obj5 = this.GetClassObject(num3);
                    if (obj5.IsPascalArray)
                    {
                        name = obj5.ImportedType.Name + "[]";
                        elementTypeName = this.symbol_table[obj5.IndexTypeId].Name;
                    }
                }
                if (elementTypeName == "")
                {
                    obj4 = this.GetClassObject(num3).FindIndexer();
                    if (obj4 == null)
                    {
                        this.scripter.CreateErrorObjectEx("CS0021. Cannot apply indexing with [] to an expression of type '{0}'.", new object[] { name });
                        goto Label_22C6;
                    }
                    num4 = this.symbol_table[obj4.Id].TypeId;
                }
                else
                {
                    for (int k = num3; k >= 0; k--)
                    {
                        if ((this.symbol_table[k].Kind == MemberKind.Type) && (this.symbol_table[k].Name == elementTypeName))
                        {
                            num4 = k;
                            break;
                        }
                    }
                }
                if (num4 == 0)
                {
                    elementTypeName = CSLite_System.GetElementTypeName(this.symbol_table[num3].FullName);
                    bool upcase = this.GetUpcase();
                    MemberObject obj6 = this.FindType(this.RecreateLevelStack(this.n), elementTypeName, upcase);
                    if (obj6 == null)
                    {
                        this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { elementTypeName });
                        goto Label_22C6;
                    }
                    num4 = obj6.Id;
                }
                this.symbol_table[this.r.res].TypeId = num4;
            }
        Label_04A6:
            if (((op == this.OP_CREATE_INDEX_OBJECT) || (op == this.OP_ADD_INDEX)) || (op == this.OP_SETUP_INDEX_OBJECT))
            {
                goto Label_0079;
            }
            for (int i = 1; i <= 2; i++)
            {
                int num13;
                if (i == 1)
                {
                    num13 = this.r.arg1;
                }
                else
                {
                    num13 = this.r.arg2;
                }
                if (this.symbol_table[num13].Kind == MemberKind.Index)
                {
                    l.Clear();
                    int avalue = this.n;
                    while (true)
                    {
                        if ((this[avalue].op == this.OP_CREATE_INDEX_OBJECT) && (this[avalue].res == num13))
                        {
                            break;
                        }
                        avalue--;
                    }
                    int num15 = this[avalue].arg1;
                    l.Add(avalue);
                    int num16 = this.symbol_table[num15].TypeId;
                    PropertyObject obj8 = this.GetClassObject(num16).FindIndexer();
                    if (obj8 != null)
                    {
                        while (true)
                        {
                            if ((this[avalue].op == this.OP_SETUP_INDEX_OBJECT) && (this[avalue].arg1 == num13))
                            {
                                break;
                            }
                            if ((this[avalue].op == this.OP_ADD_INDEX) && (this[avalue].arg1 == num13))
                            {
                                l.Add(avalue);
                            }
                            avalue++;
                        }
                        l.Add(avalue);
                        if ((this.r.op == this.OP_ASSIGN) && (i == 1))
                        {
                            if (obj8.WriteId == 0)
                            {
                                this.scripter.CreateErrorObject("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.");
                                break;
                            }
                            int num17 = this.r.arg2;
                            this.InsertOperators(this.n, obj8.ParamCount + 3);
                            this.n--;
                            this.n++;
                            this[this.n].op = this.OP_BEGIN_CALL;
                            this[this.n].arg1 = obj8.WriteId;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            for (int m = 1; m < (l.Count - 1); m++)
                            {
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = this[l[m]].arg2;
                                this[this.n].arg2 = 0;
                                this[this.n].res = obj8.WriteId;
                            }
                            this.n++;
                            this[this.n].op = this.OP_PUSH;
                            this[this.n].arg1 = num17;
                            this[this.n].arg2 = 0;
                            this[this.n].res = obj8.WriteId;
                            this.n++;
                            this[this.n].op = this.OP_PUSH;
                            this[this.n].arg1 = num15;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_CALL_BASE;
                            this[this.n].arg1 = obj8.WriteId;
                            this[this.n].arg2 = l.Count - 1;
                            this[this.n].res = 0;
                            this.r = (ProgRec) this.prog[this.n];
                            op = this.r.op;
                        }
                        else
                        {
                            if (obj8.ReadId == 0)
                            {
                                this.scripter.CreateErrorObject("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.");
                                break;
                            }
                            int num19 = this.AppVar(this.symbol_table[num13].Level, this.symbol_table[num13].TypeId);
                            if (i == 1)
                            {
                                this.r.arg1 = num19;
                            }
                            else
                            {
                                this.r.arg2 = num19;
                            }
                            this.InsertOperators(this.n, obj8.ParamCount + 3);
                            this.n--;
                            this.n++;
                            this[this.n].op = this.OP_BEGIN_CALL;
                            this[this.n].arg1 = obj8.ReadId;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            for (int num20 = 1; num20 < (l.Count - 1); num20++)
                            {
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = this[l[num20]].arg2;
                                this[this.n].arg2 = 0;
                                this[this.n].res = obj8.ReadId;
                            }
                            this.n++;
                            this[this.n].op = this.OP_PUSH;
                            this[this.n].arg1 = num15;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_CALL_BASE;
                            this[this.n].arg1 = obj8.ReadId;
                            this[this.n].arg2 = l.Count - 2;
                            this[this.n].res = num19;
                            this.symbol_table[this[this.n].res].TypeId = this.symbol_table[obj8.ReadId].TypeId;
                            this.r = (ProgRec) this.prog[this.n];
                            op = this.r.op;
                        }
                        list4.AddFrom(l);
                    }
                }
            }
            if (op == this.OP_CREATE_METHOD)
            {
                num = this.r.arg1;
                goto Label_0079;
            }
            if (op != this.OP_CALL_SIMPLE)
            {
                if ((op == this.OP_CALL) || (op == this.OP_CALL_BASE))
                {
                    this.CheckOP_CALL(classObject, num, a, list2, pos);
                    if (!this.scripter.IsError())
                    {
                        goto Label_0079;
                    }
                    goto Label_22C6;
                }
                if (op == this.OP_CHECK_STRUCT_CONSTRUCTOR)
                {
                    ClassObject obj12 = this.GetClassObject(this.r.arg1);
                    if (obj12.IsStruct && !obj12.Imported)
                    {
                        FunctionObject obj13;
                        this.r.op = this.OP_CREATE_OBJECT;
                        int num35 = obj12.FindConstructorId(null, null, out obj13);
                        if (num35 == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0143. The type '{0}' has no constructors defined.", new object[] { obj12.Name });
                        }
                        this.n++;
                        this.InsertOperators(this.n, 2);
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = this.r.res;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL;
                        this[this.n].arg1 = num35;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                    }
                    else
                    {
                        this.r.op = this.OP_NOP;
                    }
                }
                else if (op == this.OP_INSERT_STRUCT_CONSTRUCTORS)
                {
                    ClassObject obj14 = this.GetClassObject(this.r.arg1);
                    for (int num36 = 0; num36 < obj14.Members.Count; num36++)
                    {
                        MemberObject obj15 = obj14.Members[num36];
                        if (obj15.Kind == MemberKind.Field)
                        {
                            int num37 = this.symbol_table[obj15.Id].TypeId;
                            ClassObject obj16 = this.GetClassObject(num37);
                            if (obj16.IsStruct && !obj16.Imported)
                            {
                                if (obj16.IsPascalArray)
                                {
                                    this.n++;
                                    this.InsertOperators(this.n, 1);
                                    this[this.n].op = this.OP_CREATE_OBJECT;
                                    this[this.n].arg1 = num37;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = obj15.Id;
                                }
                                else
                                {
                                    FunctionObject obj17;
                                    int num38 = obj16.FindConstructorId(null, null, out obj17);
                                    if (num38 == 0)
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0143. The type '{0}' has no constructors defined.", new object[] { obj16.Name });
                                    }
                                    this.n++;
                                    this.InsertOperators(this.n, 3);
                                    this[this.n].op = this.OP_CREATE_OBJECT;
                                    this[this.n].arg1 = num37;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = obj15.Id;
                                    this.n++;
                                    this[this.n].op = this.OP_PUSH;
                                    this[this.n].arg1 = obj15.Id;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = 0;
                                    this.n++;
                                    this[this.n].op = this.OP_CALL;
                                    this[this.n].arg1 = num38;
                                    this[this.n].arg2 = 0;
                                    this[this.n].res = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (op == this.OP_CAST)
                    {
                        this.CheckOP_CAST();
                        if (!this.scripter.IsError())
                        {
                            goto Label_0079;
                        }
                        goto Label_22C6;
                    }
                    if (op == this.OP_TO_SBYTE)
                    {
                        this.symbol_table[this.r.res].TypeId = 10;
                    }
                    else if (op == this.OP_TO_BYTE)
                    {
                        this.symbol_table[this.r.res].TypeId = 3;
                    }
                    else if (op == this.OP_TO_USHORT)
                    {
                        this.symbol_table[this.r.res].TypeId = 15;
                    }
                    else if (op == this.OP_TO_SHORT)
                    {
                        this.symbol_table[this.r.res].TypeId = 11;
                    }
                    else if (op == this.OP_TO_UINT)
                    {
                        this.symbol_table[this.r.res].TypeId = 13;
                    }
                    else if (op == this.OP_TO_INT)
                    {
                        this.symbol_table[this.r.res].TypeId = 8;
                    }
                    else if (op == this.OP_TO_ULONG)
                    {
                        this.symbol_table[this.r.res].TypeId = 14;
                    }
                    else if (op == this.OP_TO_LONG)
                    {
                        this.symbol_table[this.r.res].TypeId = 9;
                    }
                    else if (op == this.OP_TO_CHAR)
                    {
                        this.symbol_table[this.r.res].TypeId = 4;
                    }
                    else if (op == this.OP_TO_FLOAT)
                    {
                        this.symbol_table[this.r.res].TypeId = 7;
                    }
                    else if (op == this.OP_TO_DOUBLE)
                    {
                        this.symbol_table[this.r.res].TypeId = 6;
                    }
                    else if (op == this.OP_TO_DECIMAL)
                    {
                        this.symbol_table[this.r.res].TypeId = 5;
                    }
                    else if (op == this.OP_TO_STRING)
                    {
                        this.symbol_table[this.r.res].TypeId = 12;
                    }
                    else if (op == this.OP_TO_BOOLEAN)
                    {
                        this.symbol_table[this.r.res].TypeId = 2;
                    }
                    else
                    {
                        if (op == this.OP_INC)
                        {
                            this.CheckOP_INC();
                            if (!this.scripter.IsError())
                            {
                                goto Label_0079;
                            }
                            goto Label_22C6;
                        }
                        if (op == this.OP_DEC)
                        {
                            this.CheckOP_DEC();
                            if (!this.scripter.IsError())
                            {
                                goto Label_0079;
                            }
                            goto Label_22C6;
                        }
                        if (op == this.OP_ASSIGN_COND_TYPE)
                        {
                            this.CheckOP_ASSIGN_COND_TYPE();
                        }
                        else if (op == this.OP_ADD_EXPLICIT_INTERFACE)
                        {
                            this.OperAddExplicitInterface();
                        }
                        else if (op == this.OP_END_CLASS)
                        {
                            stack.Pop();
                            if (stack.Count > 0)
                            {
                                int num39 = stack.Peek();
                                classObject = this.GetClassObject(num39);
                            }
                            else
                            {
                                classObject = null;
                            }
                        }
                        else
                        {
                            if (op != this.OP_CREATE_CLASS)
                            {
                                if (op == this.OP_CREATE_OBJECT)
                                {
                                    ClassObject obj27 = this.GetClassObject(this.r.arg1);
                                    if (!obj27.Abstract && !obj27.IsInterface)
                                    {
                                        goto Label_0079;
                                    }
                                    this.scripter.CreateErrorObjectEx("CS0144. Cannot create an instance of the abstract class or interface '{0}'.", new object[] { obj27.Name });
                                }
                                else
                                {
                                    if (op == this.OP_END_USING)
                                    {
                                        if ((obj2 != null) && (obj2.Id == this.r.arg1))
                                        {
                                            obj2 = null;
                                        }
                                        goto Label_0079;
                                    }
                                    if (op == this.OP_ASSIGN)
                                    {
                                        this.CheckOP_ASSIGN(obj2);
                                        if (!this.scripter.IsError())
                                        {
                                            goto Label_0079;
                                        }
                                    }
                                    else
                                    {
                                        if (op == this.OP_UNARY_MINUS)
                                        {
                                            if (!this.SetupDetailedUnaryOperator(op, "-", this.detailed_negation_operators))
                                            {
                                                goto Label_22C6;
                                            }
                                            if (this[this.n].op != this.OP_CALL_SIMPLE)
                                            {
                                                this.r = this[this.n];
                                                int num48 = this.GetTypeId(this.r.arg1);
                                                int num49 = this.GetTypeId(this.r.res);
                                                if ((num48 != num49) && IsNumericTypeId(num49))
                                                {
                                                    this.InsertNumericConversion(num49, 1);
                                                }
                                            }
                                            goto Label_0079;
                                        }
                                        if (op == this.OP_NOT)
                                        {
                                            if (this.SetupDetailedUnaryOperator(op, "!", this.detailed_logical_negation_operators))
                                            {
                                                goto Label_0079;
                                            }
                                        }
                                        else
                                        {
                                            if (op == this.OP_COMPLEMENT)
                                            {
                                                if (!this.SetupDetailedUnaryOperator(op, "~", this.detailed_bitwise_complement_operators))
                                                {
                                                    goto Label_22C6;
                                                }
                                                if (this[this.n].op != this.OP_CALL_SIMPLE)
                                                {
                                                    this.r = this[this.n];
                                                    int num50 = this.GetTypeId(this.r.arg1);
                                                    int num51 = this.GetTypeId(this.r.res);
                                                    if ((num50 != num51) && IsNumericTypeId(num51))
                                                    {
                                                        this.InsertNumericConversion(num51, 1);
                                                    }
                                                }
                                                goto Label_0079;
                                            }
                                            if (op == this.OP_PLUS)
                                            {
                                                this.CheckOP_PLUS();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_MINUS)
                                            {
                                                this.CheckOP_MINUS();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_MULT)
                                            {
                                                this.CheckOP_MULT();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_EXPONENT)
                                            {
                                                this.CheckOP_EXP();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_DIV)
                                            {
                                                this.CheckOP_DIV();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_MOD)
                                            {
                                                this.CheckOP_MOD();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_LEFT_SHIFT)
                                            {
                                                this.CheckOP_LEFT_SHIFT();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_RIGHT_SHIFT)
                                            {
                                                this.CheckOP_RIGHT_SHIFT();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_BITWISE_AND)
                                            {
                                                this.CheckOP_BITWISE_AND();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_BITWISE_OR)
                                            {
                                                this.CheckOP_BITWISE_OR();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_BITWISE_XOR)
                                            {
                                                this.CheckOP_BITWISE_XOR();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_LOGICAL_AND)
                                            {
                                                this.CheckOP_LOGICAL_AND();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else if (op == this.OP_LOGICAL_OR)
                                            {
                                                this.CheckOP_LOGICAL_OR();
                                                if (!this.scripter.IsError())
                                                {
                                                    goto Label_0079;
                                                }
                                            }
                                            else
                                            {
                                                if (op == this.OP_LT)
                                                {
                                                    if (!this.SetupDetailedBinaryOperator(op, "<", this.detailed_lt_operators))
                                                    {
                                                        goto Label_22C6;
                                                    }
                                                    this.symbol_table[this.r.res].TypeId = 2;
                                                    goto Label_0079;
                                                }
                                                if (op == this.OP_LE)
                                                {
                                                    if (!this.SetupDetailedBinaryOperator(op, "<=", this.detailed_le_operators))
                                                    {
                                                        goto Label_22C6;
                                                    }
                                                    this.symbol_table[this.r.res].TypeId = 2;
                                                    goto Label_0079;
                                                }
                                                if (op == this.OP_GT)
                                                {
                                                    if (!this.SetupDetailedBinaryOperator(op, ">", this.detailed_gt_operators))
                                                    {
                                                        goto Label_22C6;
                                                    }
                                                    this.symbol_table[this.r.res].TypeId = 2;
                                                    goto Label_0079;
                                                }
                                                if (op == this.OP_GE)
                                                {
                                                    if (!this.SetupDetailedBinaryOperator(op, ">=", this.detailed_ge_operators))
                                                    {
                                                        goto Label_22C6;
                                                    }
                                                    this.symbol_table[this.r.res].TypeId = 2;
                                                    goto Label_0079;
                                                }
                                                if (op == this.OP_EQ)
                                                {
                                                    this.CheckOP_EQ();
                                                    if (!this.scripter.IsError())
                                                    {
                                                        goto Label_0079;
                                                    }
                                                }
                                                else if (op == this.OP_NE)
                                                {
                                                    this.CheckOP_NE();
                                                    if (!this.scripter.IsError())
                                                    {
                                                        goto Label_0079;
                                                    }
                                                }
                                                else
                                                {
                                                    if (op == this.OP_IS)
                                                    {
                                                        this.symbol_table[this.r.res].TypeId = 2;
                                                        goto Label_0079;
                                                    }
                                                    if (op == this.OP_AS)
                                                    {
                                                        if (this.symbol_table[this.r.arg2].Kind != MemberKind.Type)
                                                        {
                                                            this.scripter.CreateErrorObject("CS1031. Type expected.");
                                                            goto Label_22C6;
                                                        }
                                                        ClassObject obj28 = this.GetClassObject(this.symbol_table[this.r.arg1].TypeId);
                                                        ClassObject obj29 = this.GetClassObject(this.r.arg2);
                                                        if (obj29.IsValueType)
                                                        {
                                                            this.scripter.CreateErrorObjectEx("CS0077. The as operator must be used with a reference type ('{0}' is a value type).", new object[] { obj29.Name });
                                                            goto Label_22C6;
                                                        }
                                                        if (!this.scripter.conversion.ExistsImplicitReferenceConversion(obj29, obj28))
                                                        {
                                                            this.scripter.CreateErrorObjectEx("CS0039. Cannot convert type '{0}' to '{1}'.", new object[] { obj29.Name, obj28.Name });
                                                            goto Label_22C6;
                                                        }
                                                        this.symbol_table[this.r.res].TypeId = this.r.arg2;
                                                        goto Label_0079;
                                                    }
                                                    if (op == this.OP_CREATE_REFERENCE)
                                                    {
                                                        this.CheckOP_CREATE_REFERENCE(classObject);
                                                        if (this.scripter.IsError())
                                                        {
                                                            goto Label_22C6;
                                                        }
                                                        goto Label_0079;
                                                    }
                                                    if (op == this.OP_DECLARE_LOCAL_VARIABLE)
                                                    {
                                                        this.OperDeclareLocalVariable();
                                                        goto Label_0079;
                                                    }
                                                    if (op != this.OP_ADDRESS_OF)
                                                    {
                                                        goto Label_0079;
                                                    }
                                                    this.ProcessAddressOf();
                                                    if (!this.scripter.IsError())
                                                    {
                                                        goto Label_0079;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                goto Label_22C6;
                            }
                            bool flag3 = this.GetUpcase(this.n);
                            ClassObject obj18 = this.GetClassObject(this.r.arg1);
                            if (obj18.Class_Kind == ClassKind.Enum)
                            {
                                obj2 = obj18;
                            }
                            classObject = obj18;
                            stack.Push(classObject.Id);
                            if (obj18.IsClass || obj18.IsStruct)
                            {
                                int num40 = 0;
                                for (int num41 = 0; num41 < obj18.AncestorIds.Count; num41++)
                                {
                                    int num42 = obj18.AncestorIds[num41];
                                    ClassObject obj19 = this.GetClassObject(num42);
                                    if (!obj19.IsInterface)
                                    {
                                        num40++;
                                        if (num40 > 1)
                                        {
                                            this.scripter.CreateErrorObjectEx("CS0527. '{0}' : type in interface list is not an interface.", new object[] { obj19.Name });
                                            break;
                                        }
                                    }
                                }
                                IntegerList supportedInterfaceListIds = obj18.GetSupportedInterfaceListIds();
                                if (obj18.AncestorClass.HasModifier(Modifier.Abstract))
                                {
                                    supportedInterfaceListIds.Add(obj18.AncestorClass.Id);
                                }
                                for (int num43 = 0; num43 < supportedInterfaceListIds.Count; num43++)
                                {
                                    int num44 = supportedInterfaceListIds[num43];
                                    ClassObject obj20 = this.GetClassObject(num44);
                                    for (int num45 = 0; num45 < obj20.Members.Count; num45++)
                                    {
                                        MemberObject obj21 = obj20.Members[num45];
                                        if (obj21.HasModifier(Modifier.Abstract))
                                        {
                                            if (((obj21.Kind == MemberKind.Method) || (obj21.Kind == MemberKind.Constructor)) || (obj21.Kind == MemberKind.Destructor))
                                            {
                                                bool flag4 = false;
                                                FunctionObject fy = (FunctionObject) obj21;
                                                for (int num46 = 0; num46 < obj18.Members.Count; num46++)
                                                {
                                                    MemberObject obj23 = obj18.Members[num46];
                                                    if (obj23.Public && (obj21.Kind == obj23.Kind))
                                                    {
                                                        if (CSLite_System.CompareStrings(obj21.Name, obj23.Name, flag3))
                                                        {
                                                            FunctionObject fx = (FunctionObject) obj23;
                                                            if (FunctionObject.CompareHeaders(fx, fy))
                                                            {
                                                                flag4 = true;
                                                                break;
                                                            }
                                                        }
                                                        if (obj23.ImplementsId != 0)
                                                        {
                                                            string str6 = this.symbol_table[obj23.ImplementsId].Name;
                                                            if (CSLite_System.CompareStrings(obj21.Name, str6, flag3))
                                                            {
                                                                flag4 = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (flag4)
                                                {
                                                    continue;
                                                }
                                                if (obj20.IsInterface)
                                                {
                                                    this.scripter.CreateErrorObjectEx("CS0535. '{0}' does not implement interface member '{1}'.", new object[] { obj18.FullName, obj20.FullName + "." + fy.Name });
                                                }
                                                else if (obj20.IsClass)
                                                {
                                                    this.scripter.CreateErrorObjectEx("CS0534. '{0}' does not implement inherited abstract member '{1}'.", new object[] { obj18.FullName, obj20.FullName + "." + fy.Name });
                                                }
                                                break;
                                            }
                                            if (obj21.Kind == MemberKind.Property)
                                            {
                                                bool flag5 = false;
                                                PropertyObject obj25 = (PropertyObject) obj21;
                                                for (int num47 = 0; num47 < obj18.Members.Count; num47++)
                                                {
                                                    MemberObject obj26 = obj18.Members[num47];
                                                    if (obj26.Public && (obj21.Kind == obj26.Kind))
                                                    {
                                                        if (CSLite_System.CompareStrings(obj21.Name, obj26.Name, flag3))
                                                        {
                                                            flag5 = true;
                                                            break;
                                                        }
                                                        if (obj26.ImplementsId != 0)
                                                        {
                                                            string str7 = this.symbol_table[obj26.ImplementsId].Name;
                                                            if (CSLite_System.CompareStrings(obj21.Name, str7, flag3))
                                                            {
                                                                flag5 = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!flag5)
                                                {
                                                    if (obj20.IsInterface)
                                                    {
                                                        this.scripter.CreateErrorObjectEx("CS0535. '{0}' does not implement interface member '{1}'.", new object[] { obj18.FullName, obj20.FullName + "." + obj25.Name });
                                                    }
                                                    else if (obj20.IsClass)
                                                    {
                                                        this.scripter.CreateErrorObjectEx("CS0534. '{0}' does not implement inherited abstract member '{1}'.", new object[] { obj18.FullName, obj20.FullName + "." + obj25.Name });
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                goto Label_0079;
            }
            this.r.op = this.OP_CALL;
            int id = this.r.arg1;
            if (this.symbol_table[id].Kind != MemberKind.Method)
            {
                goto Label_0079;
            }
            int num22 = this.r.arg2;
            a.Clear();
            list2.Clear();
            pos.Clear();
            if (num22 > 0)
            {
                int num23 = this.n - 1;
                do
                {
                    if ((this[num23].op == this.OP_PUSH) && (this[num23].res == id))
                    {
                        pos.Add(num23);
                        a.Add(this[num23].arg1);
                        list2.Add(this[num23].arg2);
                        if (a.Count == num22)
                        {
                            goto Label_0C8B;
                        }
                    }
                    num23--;
                }
                while (num23 != 0);
                this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                return;
            }
        Label_0C8B:
            obj9 = this.GetFunctionObject(id);
            bool flag2 = true;
            for (int j = 0; j < num22; j++)
            {
                int paramId = obj9.GetParamId(j);
                int num26 = a[j];
                if (!obj9.Imported)
                {
                    flag2 = ((int)obj9.GetParamMod(j) == list2[j]);
                }
                else
                {
                    flag2 = true;
                }
                if (!flag2)
                {
                    this.n = pos[j];
                    ParamMod paramMod = obj9.GetParamMod(j);
                    ParamMod mod2 = (ParamMod) list2[j];
                    int num27 = this.symbol_table[paramId].TypeId;
                    int num28 = this.symbol_table[num26].TypeId;
                    string str4 = this.symbol_table[num27].Name;
                    string str5 = this.symbol_table[num28].Name;
                    switch (paramMod)
                    {
                        case ParamMod.RetVal:
                            str4 = "ref " + str4;
                            break;

                        case ParamMod.Out:
                            str4 = "out " + str4;
                            break;
                    }
                    if (mod2 == ParamMod.RetVal)
                    {
                        str5 = "ref " + str5;
                    }
                    else if (mod2 == ParamMod.Out)
                    {
                        str5 = "out " + str5;
                    }
                    this.scripter.CreateErrorObjectEx("CS0029. Cannot impllicitly convert type '{0}' to '{1}'.", new object[] { str4, str5 });
                    break;
                }
                flag2 = this.scripter.MatchAssignment(paramId, num26);
                if (!flag2)
                {
                    this.n = pos[j];
                    int num29 = this.symbol_table[paramId].TypeId;
                    int num30 = this.symbol_table[num26].TypeId;
                    ClassObject obj10 = this.GetClassObject(num29);
                    ClassObject obj11 = this.GetClassObject(num30);
                    int num31 = obj10.FindOverloadableImplicitOperatorId(num26, paramId);
                    if (num31 > 0)
                    {
                        int currMethodId = this.GetCurrMethodId();
                        int num33 = this.AppVar(currMethodId, num29);
                        int res = this[this.n].res;
                        this[this.n].arg1 = num33;
                        this.InsertOperators(this.n, 3);
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num26;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num31;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = obj10.Id;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL_SIMPLE;
                        this[this.n].arg1 = num31;
                        this[this.n].arg2 = 1;
                        this[this.n].res = num33;
                        while (this[this.n].arg1 != res)
                        {
                            this.n++;
                        }
                        goto Label_0079;
                    }
                    this.scripter.CreateErrorObjectEx("CS0029. Cannot impllicitly convert type '{0}' to '{1}'.", new object[] { obj11.Name, obj10.Name });
                    break;
                }
            }
            if (flag2)
            {
                goto Label_0079;
            }
        Label_22C6:
            num52 = 0;
            while (num52 < list4.Count)
            {
                this.n = list4[num52];
                this[this.n].op = this.OP_NOP;
                num52++;
            }
        }

        private bool CompareDelegates(ObjectObject d1, ObjectObject d2)
        {
            if (d1.InvocationCount == d2.InvocationCount)
            {
                object obj2;
                object obj3;
                FunctionObject obj4;
                FunctionObject obj5;
                bool flag = d1.FindFirstInvocation(out obj2, out obj4);
                for (flag = d2.FindFirstInvocation(out obj3, out obj5); flag; flag = d2.FindNextInvocation(out obj3, out obj5))
                {
                    if ((obj2 != obj3) || (obj4 != obj5))
                    {
                        return false;
                    }
                    flag = d1.FindNextInvocation(out obj2, out obj4);
                }
                return true;
            }
            return false;
        }

        private void ConvertCallToIndexAccess(IntegerList a, IntegerList pos)
        {
            int num = this.r.arg1;
            int res = this.r.res;
            this.symbol_table[res].Level = num;
            this.symbol_table[res].Kind = MemberKind.Index;
            int n = pos[0];
            do
            {
                n--;
            }
            while (this[n].op != this.OP_NOP);
            int num4 = n;
            this[n].op = this.OP_CREATE_INDEX_OBJECT;
            this[n].arg1 = num;
            this[n].arg2 = 0;
            this[n].res = res;
            for (int i = 0; i < pos.Count; i++)
            {
                n = pos[i];
                this[n].op = this.OP_ADD_INDEX;
                this[n].arg1 = res;
                this[n].arg2 = a[i];
                this[n].res = 0;
            }
            n = this.n - 1;
            this[n].op = this.OP_NOP;
            this[n].arg1 = 0;
            this[n].arg2 = 0;
            this[n].res = 0;
            n = this.n;
            this[n].op = this.OP_SETUP_INDEX_OBJECT;
            this[n].arg1 = res;
            this[n].arg2 = 0;
            this[n].res = 0;
            this.n = num4 - 1;
        }

        private void ConvertToDefaultPropertyCall(PropertyObject p, IntegerList a, IntegerList pos)
        {
            int num3;
            FunctionObject functionObject;
            int num = this.r.arg1;
            int num2 = 0;
            if ((this[this.n + 1].op == this.OP_ASSIGN) && (this[this.n + 1].arg1 == this[this.n].res))
            {
                num2 = this.n + 1;
            }
            if (num2 > 0)
            {
                if (p.WriteId == 0)
                {
                    this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { p.Name });
                }
                else
                {
                    functionObject = this.GetFunctionObject(p.WriteId);
                    int num4 = this[num2].arg2;
                    this.r.arg1 = p.WriteId;
                    this[this.n - 1].arg1 = num;
                    this.r.arg2++;
                    if (functionObject.ParamCount != this.r.arg2)
                    {
                        this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this.r.arg2 });
                    }
                    for (num3 = 0; num3 < pos.Count; num3++)
                    {
                        this[pos[num3]].res = p.WriteId;
                    }
                    this.InsertOperators(this.n - 1, 1);
                    this[this.n - 1].op = this.OP_PUSH;
                    this[this.n - 1].arg1 = num4;
                    this[this.n - 1].arg2 = 0;
                    this[this.n - 1].res = p.WriteId;
                    this.n++;
                    this[this.n + 1].op = this.OP_NOP;
                }
            }
            else if (p.ReadId == 0)
            {
                this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { p.Name });
            }
            else
            {
                this.r.arg1 = p.ReadId;
                if (this.r.res >= 0)
                {
                    this.symbol_table[this.r.res].TypeId = this.symbol_table[this.r.arg1].TypeId;
                }
                this[this.n - 1].arg1 = num;
                for (num3 = 0; num3 < pos.Count; num3++)
                {
                    this[pos[num3]].res = p.ReadId;
                }
                functionObject = this.GetFunctionObject(p.ReadId);
                if (functionObject.ParamCount != this.r.arg2)
                {
                    this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this.r.arg2 });
                }
            }
        }

        private void ConvertToPropertyCall(IntegerList a, IntegerList pos)
        {
            int typeId;
            int num3;
            FunctionObject functionObject;
            PropertyObject val = (PropertyObject) this.symbol_table[this.r.arg1].Val;
            if (val.Static)
            {
                typeId = this.symbol_table[this.r.arg1].TypeId;
            }
            else
            {
                typeId = this.r.arg1;
            }
            int num2 = 0;
            if ((this[this.n + 1].op == this.OP_ASSIGN) && (this[this.n + 1].arg1 == this[this.n].res))
            {
                num2 = this.n + 1;
            }
            if (num2 > 0)
            {
                if (val.WriteId == 0)
                {
                    this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { val.Name });
                }
                else
                {
                    functionObject = this.GetFunctionObject(val.WriteId);
                    int num4 = this[num2].arg2;
                    this.r.arg1 = val.WriteId;
                    this[this.n - 1].arg1 = typeId;
                    this.r.arg2++;
                    if (functionObject.ParamCount != this.r.arg2)
                    {
                        this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this.r.arg2 });
                    }
                    for (num3 = 0; num3 < pos.Count; num3++)
                    {
                        this[pos[num3]].res = val.WriteId;
                    }
                    this.InsertOperators(this.n - 1, 1);
                    this[this.n - 1].op = this.OP_PUSH;
                    this[this.n - 1].arg1 = num4;
                    this[this.n - 1].arg2 = 0;
                    this[this.n - 1].res = val.WriteId;
                    this.n++;
                    this[this.n + 1].op = this.OP_NOP;
                }
            }
            else if (val.ReadId == 0)
            {
                this.scripter.CreateErrorObjectEx("CS0154. The property or indexer '{0}' cannot be used in this context because it lacks the get accessor.", new object[] { val.Name });
            }
            else
            {
                this.r.arg1 = val.ReadId;
                this[this.n - 1].arg1 = typeId;
                for (num3 = 0; num3 < pos.Count; num3++)
                {
                    this[pos[num3]].res = val.ReadId;
                }
                functionObject = this.GetFunctionObject(val.ReadId);
                if (functionObject.ParamCount != this.r.arg2)
                {
                    this.scripter.CreateErrorObjectEx("CS1501. No overload for method '{0}' takes '{1}' arguments.", new object[] { functionObject.FullName, this.r.arg2 });
                }
            }
        }

        public void CreateClassObjects()
        {
            for (int i = 1; i <= this.Card; i++)
            {
                this.r = (ProgRec) this.prog[i];
                int op = this.r.op;
                this.n = i;
                if (op == this.OP_CREATE_NAMESPACE)
                {
                    this.OperCreateNamespace();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_CREATE_USING_ALIAS)
                {
                    this.OperCreateUsingAlias();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_CREATE_CLASS)
                {
                    this.OperCreateClass();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_CREATE_FIELD)
                {
                    this.OperCreateField();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_CREATE_PROPERTY)
                {
                    this.OperCreateProperty();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_CREATE_EVENT)
                {
                    this.OperCreateEvent();
                    if (!this.scripter.IsError())
                    {
                        continue;
                    }
                    break;
                }
                if (op == this.OP_ADD_EVENT_FIELD)
                {
                    this.OperAddEventField();
                }
                else if (op == this.OP_ADD_READ_ACCESSOR)
                {
                    this.OperAddReadAccessor();
                }
                else if (op == this.OP_ADD_WRITE_ACCESSOR)
                {
                    this.OperAddWriteAccessor();
                }
                else if (op == this.OP_SET_DEFAULT)
                {
                    this.OperSetDefault();
                }
                else if (op == this.OP_ADD_ADD_ACCESSOR)
                {
                    this.OperAddAddAccessor();
                }
                else if (op == this.OP_ADD_REMOVE_ACCESSOR)
                {
                    this.OperAddRemoveAccessor();
                }
                else if (op == this.OP_ADD_MIN_VALUE)
                {
                    this.OperAddMinValue();
                }
                else if (op == this.OP_ADD_MAX_VALUE)
                {
                    this.OperAddMaxValue();
                }
                else if (op == this.OP_CREATE_METHOD)
                {
                    this.OperCreateMethod();
                }
                else if (op == this.OP_ADD_PATTERN)
                {
                    this.OperAddPattern();
                }
                else if (op == this.OP_ADD_PARAM)
                {
                    this.OperAddParam();
                }
                else if (op == this.OP_ADD_PARAMS)
                {
                    this.OperAddParams();
                }
                else if (op == this.OP_ADD_DEFAULT_VALUE)
                {
                    this.OperAddDefaultValue();
                }
                else if (op == this.OP_INIT_METHOD)
                {
                    this.OperInitMethod();
                }
                else if (op == this.OP_END_METHOD)
                {
                    this.OperEndMethod();
                }
                else if (op == this.OP_ADD_MODIFIER)
                {
                    this.OperAddModifier();
                }
            }
        }

        public void CreatePatternMethod(Delegate instance)
        {
            Type t = instance.GetType();
            int id = this.symbol_table.RegisterType(t, true);
            if (this.GetClassObject(id).PatternMethod.Init == null)
            {
            }
        }

        public void CreatePatternMethod(FunctionObject p, FunctionObject g)
        {
            this.symbol_table[p.Id].TypeId = this.symbol_table[g.Id].TypeId;
            this.symbol_table[p.ResultId].TypeId = this.symbol_table[g.Id].TypeId;
            for (int i = 0; i < g.ParamCount; i++)
            {
                int num2 = g.Param_Ids[i];
                int avalue = this.AppVar(p.Id);
                this.symbol_table[avalue].TypeId = this.symbol_table[num2].TypeId;
                this.symbol_table[avalue].Name = this.symbol_table[num2].Name;
                p.Param_Ids.Add(avalue);
                p.Param_Mod.Add(0);
            }
            int num4 = this.AppVar(p.Id);
            int res = this.AppVar(p.Id);
            int resultId = p.ResultId;
            int thisId = p.ThisId;
            int num8 = this.AppLabel();
            int num9 = this.AppLabel();
            int num10 = this.AddInstruction(this.OP_FIND_FIRST_DELEGATE, thisId, num4, res);
            this.AddInstruction(this.OP_GO_NULL, num8, num4, 0);
            for (int j = 0; j < p.ParamCount; j++)
            {
                this.AddInstruction(this.OP_PUSH, p.Param_Ids[j], p.Param_Mod[j], num4);
            }
            this.AddInstruction(this.OP_PUSH, res, 0, 0);
            this.AddInstruction(this.OP_CALL_SIMPLE, num4, p.ParamCount, resultId);
            this.SetLabelHere(num9);
            this.AddInstruction(this.OP_FIND_NEXT_DELEGATE, thisId, num4, res);
            this.AddInstruction(this.OP_GO_NULL, num8, num4, 0);
            for (int k = 0; k < p.ParamCount; k++)
            {
                this.AddInstruction(this.OP_PUSH, p.Param_Ids[k], p.Param_Mod[k], num4);
            }
            this.AddInstruction(this.OP_PUSH, res, 0, 0);
            this.AddInstruction(this.OP_CALL_SIMPLE, num4, p.ParamCount, resultId);
            this.AddInstruction(this.OP_GO, num9, 0, 0);
            this.SetLabelHere(num8);
            this.AddInstruction(this.OP_RET, p.Id, 0, 0);
            p.Init = this[num10];
        }

        public void Dump(string FileName)
        {
        }

        private int EvalLabel(int l)
        {
            int level = this.symbol_table[l].Level;
            string name = this.symbol_table[l].Name;
            for (int i = 1; i <= this.card; i++)
            {
                if ((this[i].op == this.OP_DECLARE_LOCAL_VARIABLE) && (this[i].arg2 == level))
                {
                    int num3 = this[i].arg1;
                    if (this.symbol_table[num3].Kind == MemberKind.Label)
                    {
                        string str2 = this.symbol_table[num3].Name;
                        if (name == str2)
                        {
                            return num3;
                        }
                    }
                }
            }
            return 0;
        }

        private MemberObject FindType(IntegerStack l, string type_name, bool upcase)
        {
            int num;
            string str = CSLite_System.ExtractPrefixName(type_name, out num);
            if (num >= 0)
            {
                if (str == "bool")
                {
                    str = "Boolean" + type_name.Substring(num);
                }
                else if (str == "byte")
                {
                    str = "Byte" + type_name.Substring(num);
                }
                else if (str == "char")
                {
                    str = "Char" + type_name.Substring(num);
                }
                else if (str == "decimal")
                {
                    str = "Decimal" + type_name.Substring(num);
                }
                else if (str == "double")
                {
                    str = "double" + type_name.Substring(num);
                }
                else if (str == "single")
                {
                    str = "Single" + type_name.Substring(num);
                }
                else if (str == "int")
                {
                    str = "Int32" + type_name.Substring(num);
                }
                else if (str == "long")
                {
                    str = "Int64" + type_name.Substring(num);
                }
                else if (str == "sbyte")
                {
                    str = "SByte" + type_name.Substring(num);
                }
                else if (str == "short")
                {
                    str = "Int16" + type_name.Substring(num);
                }
                else if (str == "string")
                {
                    str = "String" + type_name.Substring(num);
                }
                else if (str == "uint")
                {
                    str = "UInt32" + type_name.Substring(num);
                }
                else if (str == "ulong")
                {
                    str = "UInt64" + type_name.Substring(num);
                }
                else if (str == "ushort")
                {
                    str = "UInt16" + type_name.Substring(num);
                }
                else
                {
                    str = type_name;
                }
            }
            string typeName = str;
            Type t = Type.GetType(typeName);
            if (t == null)
            {
                t = this.scripter.FindAvailableType(typeName, upcase);
            }
            if (t != null)
            {
                int id = this.symbol_table.RegisterType(t, true);
                return this.GetMemberObject(id);
            }
            for (int i = l.Count - 1; i >= 0; i--)
            {
                int num4 = l[i];
                typeName = this.symbol_table[num4].FullName + "." + str;
                t = Type.GetType(typeName);
                if (t == null)
                {
                    t = this.scripter.FindAvailableType(typeName, upcase);
                }
                if (t != null)
                {
                    int num5 = this.symbol_table.RegisterType(t, true);
                    return this.GetMemberObject(num5);
                }
            }
            return null;
        }

        public ClassObject GetClassObject(int id)
        {
            return this.scripter.GetClassObject(id);
        }

        public ClassObject GetClassObjectEx(int id)
        {
            return this.scripter.GetClassObjectEx(id);
        }

        private ClassObject GetCurrentClass(IntegerStack l)
        {
            for (int i = l.Count - 1; i >= 0; i--)
            {
                MemberObject memberObject = this.GetMemberObject(l[i]);
                if (memberObject.Kind == MemberKind.Type)
                {
                    return (memberObject as ClassObject);
                }
            }
            return null;
        }

        public ProgRec GetCurrentIstruction()
        {
            return this[this.n];
        }

        private int GetCurrMethodId()
        {
            int n = this.n;
            do
            {
                if (this[n].op == this.OP_INIT_METHOD)
                {
                    return n;
                }
                n--;
            }
            while (n > 0);
            return 0;
        }

        public int GetErrorLineNumber(int j)
        {
            bool flag = this[j].op == this.OP_SEPARATOR;
            int num = j;
            while (num > 0)
            {
                if (this[num].op == this.OP_SEPARATOR)
                {
                    if ((!flag || (num <= 1)) || (this[num - 1].op != this.OP_SEPARATOR))
                    {
                        if (flag)
                        {
                            return (this[num].arg2 - 1);
                        }
                        return this[num].arg2;
                    }
                    num--;
                }
                else
                {
                    num--;
                }
            }
            return -1;
        }

        public EventObject GetEventObject(int id)
        {
            return this.scripter.GetEventObject(id);
        }

        private bool GetExplicit(int i)
        {
            for (int j = i; j >= 1; j--)
            {
                if (this[j].op == this.OP_EXPLICIT_ON)
                {
                    return true;
                }
                if (this[j].op == this.OP_EXPLICIT_OFF)
                {
                    return false;
                }
            }
            return false;
        }

        public FieldObject GetFieldObject(int id)
        {
            return (FieldObject) this.GetVal(id);
        }

        public FunctionObject GetFunctionObject(int id)
        {
            return (FunctionObject) this.GetVal(id);
        }

        public IndexObject GetIndexObject(int id)
        {
            return this.scripter.GetIndexObject(id);
        }

        public CSLite_Language GetLanguage(int j)
        {
            int num = j;
            while (this[num].op != this.OP_BEGIN_MODULE)
            {
                num--;
            }
            return (CSLite_Language) this[num].arg2;
        }

        public int GetLineNumber(int j)
        {
            for (int i = j; i > 0; i--)
            {
                if (this[i].op == this.OP_SEPARATOR)
                {
                    return this[i].arg2;
                }
            }
            return -1;
        }

        public MemberObject GetMemberObject(int id)
        {
            return this.scripter.GetMemberObject(id);
        }

        public  Module GetModule(int j)
        {
            ProgRec rec;
            int num = j;
            do
            {
                rec = (ProgRec) this.prog[num];
                if (rec.op == this.OP_BEGIN_MODULE)
                {
                    return this.scripter.module_list.GetModule(rec.arg1);
                }
                num--;
            }
            while (num > 0);
            return this.scripter.module_list.GetModule(rec.arg1);
        }

        public ObjectObject GetObjectObject(int id)
        {
            return this.scripter.ToScriptObject(this.GetValue(id));
        }

        public string GetOperName(int op, int l)
        {
            return CSLite_System.Norm(this.Operators[-op], l);
        }

        public PropertyObject GetPropertyObject(int id)
        {
            return this.scripter.GetPropertyObject(id);
        }

        private bool GetStrict(int i)
        {
            for (int j = i; j >= 1; j--)
            {
                if (this[j].op == this.OP_STRICT_ON)
                {
                    return true;
                }
                if (this[j].op == this.OP_STRICT_OFF)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetSubId(int j)
        {
            int num = j;
            do
            {
                ProgRec rec = (ProgRec) this.prog[num];
                if (rec.op == this.OP_CREATE_METHOD)
                {
                    return rec.arg1;
                }
                num--;
            }
            while (num > 0);
            return -1;
        }

        public int GetTypeId(int id)
        {
            return this.symbol_table[id].TypeId;
        }

        private bool GetUpcase()
        {
            return this.GetUpcase(this.n);
        }

        private bool GetUpcase(int i)
        {
            if (i <= 1)
            {
                return true;
            }
            if (this[i].upcase == Upcase.Yes)
            {
                return true;
            }
            if (this[i].upcase == Upcase.No)
            {
                return false;
            }
            return this.GetUpcase(i - 1);
        }

        public object GetVal(int id)
        {
            return this.symbol_table[id].Val;
        }

        public object GetValue(int id)
        {
            return this.symbol_table[id].Value;
        }

        public bool GetValueAsBool(int id)
        {
            return this.symbol_table[id].ValueAsBool;
        }

        public byte GetValueAsByte(int id)
        {
            return this.symbol_table[id].ValueAsByte;
        }

        public char GetValueAsChar(int id)
        {
            return this.symbol_table[id].ValueAsChar;
        }

        public decimal GetValueAsDecimal(int id)
        {
            return this.symbol_table[id].ValueAsDecimal;
        }

        public double GetValueAsDouble(int id)
        {
            return this.symbol_table[id].ValueAsDouble;
        }

        public float GetValueAsFloat(int id)
        {
            return this.symbol_table[id].ValueAsFloat;
        }

        public int GetValueAsInt(int id)
        {
            return this.symbol_table[id].ValueAsInt;
        }

        public long GetValueAsLong(int id)
        {
            return this.symbol_table[id].ValueAsLong;
        }

        public short GetValueAsShort(int id)
        {
            return this.symbol_table[id].ValueAsShort;
        }

        public string GetValueAsString(int id)
        {
            return this.symbol_table[id].ValueAsString;
        }

        public bool HasBreakpoint(int i)
        {
            return this.breakpoint_list.HasBreakpoint(i);
        }

        private void InsertActualArray(FunctionObject f, int curr_level, IntegerList pos, IntegerList a)
        {
            this[this.n].arg2 = f.ParamCount;
            int num = a.Count - f.ParamCount;
            int typeId = this.symbol_table[f.ParamsId].TypeId;
            string elementTypeName = CSLite_System.GetElementTypeName(this.symbol_table[typeId].Name);
            int num3 = this.scripter.GetTypeId(elementTypeName);
            int num4 = 0;
            if ((num3 == 4) && (f.ParamCount == 1))
            {
                num4 = this.AppVar(curr_level, num3);
                this.n = pos[pos.Count - 1];
                int num5 = this[this.n].arg1;
                this.InsertOperators(this.n, 1);
                this[this.n].op = this.OP_TO_CHAR_ARRAY;
                this[this.n].arg1 = num4;
                this[this.n].arg2 = num5;
                this[this.n].res = num4;
                this.n++;
                this[this.n].arg1 = num4;
                this.n++;
                this.n++;
            }
            else
            {
                int num6 = this.AppVar(curr_level, typeId);
                int avalue = this.symbol_table.AppIntegerConst(num + 1);
                ClassObject classObject = this.GetClassObject(typeId);
                new IntegerList(false).Add(avalue);
                new IntegerList(false).Add(0);
                this.n--;
                this.InsertOperators(this.n, (4 + (4 * (num + 1))) + 1);
                this[this.n].op = this.OP_CREATE_OBJECT;
                this[this.n].arg1 = typeId;
                this[this.n].arg2 = 0;
                this[this.n].res = num6;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = avalue;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = num6;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_CREATE_ARRAY_INSTANCE;
                this[this.n].arg1 = num3;
                this[this.n].arg2 = 1;
                this[this.n].res = 0;
                num4 = 0;
                int num8 = 0;
                for (int i = 1; i <= a.Count; i++)
                {
                    if (i >= f.ParamCount)
                    {
                        this[pos[i - 1]].op = this.OP_NOP;
                        num4 = this.AppVar(curr_level, num3);
                        this.symbol_table[num4].Kind = MemberKind.Index;
                        int num10 = this.symbol_table.AppIntegerConst(num8++);
                        this.n++;
                        this[this.n].op = this.OP_CREATE_INDEX_OBJECT;
                        this[this.n].arg1 = num6;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num4;
                        this.n++;
                        this[this.n].op = this.OP_ADD_INDEX;
                        this[this.n].arg1 = num4;
                        this[this.n].arg2 = num10;
                        this[this.n].res = num6;
                        this.n++;
                        this[this.n].op = this.OP_SETUP_INDEX_OBJECT;
                        this[this.n].arg1 = num4;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num6;
                        this.n++;
                        this[this.n].op = this.OP_ASSIGN;
                        this[this.n].arg1 = num4;
                        this[this.n].arg2 = a[i - 1];
                        this[this.n].res = num4;
                    }
                }
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = num6;
                this[this.n].arg2 = 0;
                this[this.n].res = f.Id;
                this.n++;
                this.n++;
            }
        }

        public void InsertEventHandlers()
        {
            CSLite_Language cSharp = CSLite_Language.CSharp;
            this.n = 0;
        Label_000E:
            this.n++;
            if (this.n < this.card)
            {
                if (this[this.n].op == this.OP_BEGIN_MODULE)
                {
                    cSharp = (CSLite_Language) this[this.n].arg2;
                }
                if ((cSharp == CSLite_Language.VB) && (this[this.n].op == this.OP_ASSIGN))
                {
                    int res = this[this.n].res;
                    if ((this.symbol_table[res].Kind == MemberKind.Field) && this.GetFieldObject(res).HasModifier(Modifier.WithEvents))
                    {
                        for (int i = 1; i <= this.card; i++)
                        {
                            if ((this[i].op != this.OP_ADD_HANDLES) || (this[i].arg2 != res))
                            {
                                continue;
                            }
                            int n = this.n;
                            int num4 = this[this.n].arg2;
                            bool flag = false;
                            for (int j = this.n; j >= 1; j--)
                            {
                                if ((this[j].op == this.OP_CREATE_OBJECT) && (this[j].res == num4))
                                {
                                    flag = true;
                                    this.n = j;
                                    res = num4;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                int id = this[i].arg1;
                                int typeId = this.symbol_table[res].TypeId;
                                ClassObject classObject = this.GetClassObject(typeId);
                                int nameIndex = this.symbol_table[this[i].res].NameIndex;
                                MemberObject memberByNameIndex = classObject.GetMemberByNameIndex(nameIndex, this.GetUpcase(this.n));
                                if (memberByNameIndex == null)
                                {
                                    string name = this.symbol_table[this[i].res].Name;
                                    this.n = i;
                                    this.scripter.CreateErrorObjectEx("Undeclared identifier '{0}'", new object[] { name });
                                    return;
                                }
                                if (memberByNameIndex.Kind != MemberKind.Event)
                                {
                                    string str2 = this.symbol_table[this[i].res].Name;
                                    this.n = i;
                                    this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { str2, memberByNameIndex.Kind.ToString(), "event" });
                                    return;
                                }
                                EventObject obj5 = memberByNameIndex as EventObject;
                                int addId = obj5.AddId;
                                int num10 = this.symbol_table[obj5.Id].TypeId;
                                int num11 = this.AppVar(this.symbol_table[res].Level, num10);
                                int num12 = 0;
                                FunctionObject functionObject = this.GetFunctionObject(id);
                                if (functionObject.Static)
                                {
                                    num12 = functionObject.Owner.Id;
                                }
                                else
                                {
                                    num12 = res;
                                }
                                this.InsertOperators(this.n + 1, 8);
                                this.n++;
                                this[this.n].op = this.OP_CREATE_OBJECT;
                                this[this.n].arg1 = num10;
                                this[this.n].arg2 = 0;
                                this[this.n].res = num11;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = num12;
                                this[this.n].arg2 = 0;
                                this[this.n].res = num10;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = id;
                                this[this.n].arg2 = 0;
                                this[this.n].res = num10;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = num11;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.n++;
                                this[this.n].op = this.OP_SETUP_DELEGATE;
                                this[this.n].arg1 = 0;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = num11;
                                this[this.n].arg2 = 0;
                                this[this.n].res = addId;
                                this.n++;
                                this[this.n].op = this.OP_PUSH;
                                this[this.n].arg1 = res;
                                this[this.n].arg2 = 0;
                                this[this.n].res = 0;
                                this.n++;
                                this[this.n].op = this.OP_CALL;
                                this[this.n].arg1 = addId;
                                this[this.n].arg2 = 1;
                                this[this.n].res = 0;
                                this.n = n + 8;
                            }
                        }
                    }
                }
                goto Label_000E;
            }
            this.n = 0;
        Label_0609:
            this.n++;
            if (this.n < this.card)
            {
                if (this[this.n].op == this.OP_BEGIN_MODULE)
                {
                    cSharp = (CSLite_Language) this[this.n].arg2;
                }
                if (this[this.n].op != this.OP_ADD_HANDLES)
                {
                    goto Label_0609;
                }
                int num13 = this[this.n].arg1;
                FunctionObject obj7 = this.GetFunctionObject(num13);
                if (obj7.Static)
                {
                    goto Label_0609;
                }
                int num14 = this.symbol_table[this[this.n].res].NameIndex;
                int num15 = this.symbol_table[this[this.n].arg2].TypeId;
                MemberObject obj9 = this.GetClassObject(num15).GetMemberByNameIndex(num14, this.GetUpcase(this.n));
                if (obj9 == null)
                {
                    string str3 = this.symbol_table[this[this.n].res].Name;
                    this.scripter.CreateErrorObjectEx("Undeclared identifier '{0}'", new object[] { str3 });
                }
                else if (obj9.Kind != MemberKind.Event)
                {
                    string str4 = this.symbol_table[this[this.n].res].Name;
                    this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { str4, obj9.Kind.ToString(), "event" });
                }
                else
                {
                    EventObject obj10 = obj9 as EventObject;
                    int num16 = obj10.AddId;
                    int num17 = this.symbol_table[obj10.Id].TypeId;
                    int num18 = obj7.Owner.FindConstructorId();
                    if (num18 == 0)
                    {
                        this.scripter.CreateErrorObject("CS0001. Internal compiler error.");
                    }
                    else
                    {
                        int num19 = 0;
                        for (int k = 0; k <= this.card; k++)
                        {
                            if ((this[k].op == this.OP_END_METHOD) && (this[k].arg1 == num18))
                            {
                                num19 = k;
                                break;
                            }
                        }
                        int thisId = this.symbol_table.GetThisId(num18);
                        int num22 = this.AppVar(thisId, this.symbol_table[this[this.n].arg2].TypeId);
                        this.symbol_table[num22].Name = this.symbol_table[this[this.n].arg2].Name;
                        this.symbol_table[num22].Kind = MemberKind.Ref;
                        int num23 = this.AppVar(num18, num17);
                        int num24 = thisId;
                        int num25 = this.n;
                        this.n = num19;
                        this.InsertOperators(this.n, 9);
                        this[this.n].op = this.OP_CREATE_REFERENCE;
                        this[this.n].arg1 = thisId;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num22;
                        this.n++;
                        this[this.n].op = this.OP_CREATE_OBJECT;
                        this[this.n].arg1 = num17;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num23;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num24;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num17;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num13;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num17;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num23;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_SETUP_DELEGATE;
                        this[this.n].arg1 = 0;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num23;
                        this[this.n].arg2 = 0;
                        this[this.n].res = num16;
                        this.n++;
                        this[this.n].op = this.OP_PUSH;
                        this[this.n].arg1 = num22;
                        this[this.n].arg2 = 0;
                        this[this.n].res = 0;
                        this.n++;
                        this[this.n].op = this.OP_CALL;
                        this[this.n].arg1 = num16;
                        this[this.n].arg2 = 1;
                        this[this.n].res = 0;
                        this.n = num25 + 9;
                        goto Label_0609;
                    }
                }
            }
        }

        private void InsertNumericConversion(int dest_type_id, int arg_number)
        {
            if (this[this.n - 1].op == this.OP_NOP)
            {
                this.n--;
            }
            else
            {
                this.InsertOperators(this.n, 1);
            }
            int num = this.AppVar(this.symbol_table[this.r.res].Level, this.symbol_table[this.r.res].TypeId);
            this[this.n].arg1 = 0;
            this[this.n].res = num;
            if (arg_number == 1)
            {
                this[this.n].arg2 = this.r.arg1;
                this.r.arg1 = num;
            }
            else
            {
                this[this.n].arg2 = this.r.arg2;
                this.r.arg2 = num;
            }
            switch (dest_type_id)
            {
                case 10:
                    this[this.n].op = this.OP_TO_SBYTE;
                    break;

                case 3:
                    this[this.n].op = this.OP_TO_BYTE;
                    break;

                case 13:
                    this[this.n].op = this.OP_TO_UINT;
                    break;

                case 8:
                    this[this.n].op = this.OP_TO_INT;
                    break;

                case 15:
                    this[this.n].op = this.OP_TO_USHORT;
                    break;

                case 11:
                    this[this.n].op = this.OP_TO_SHORT;
                    break;

                case 14:
                    this[this.n].op = this.OP_TO_ULONG;
                    break;

                case 9:
                    this[this.n].op = this.OP_TO_LONG;
                    break;

                case 4:
                    this[this.n].op = this.OP_TO_CHAR;
                    break;

                case 7:
                    this[this.n].op = this.OP_TO_FLOAT;
                    break;

                case 5:
                    this[this.n].op = this.OP_TO_DECIMAL;
                    break;

                case 6:
                    this[this.n].op = this.OP_TO_DOUBLE;
                    break;
            }
            this.n++;
        }

        private void InsertOperators(int pos, int number)
        {
            for (int i = 0; i < number; i++)
            {
                ProgRec rec = new ProgRec();
                rec.op = this.OP_NOP;
                this.prog.Insert(pos, rec);
            }
            this.Card += number;
        }

        public static bool IsFloatingPointTypeId(int id)
        {
            return ((id == 7) || (id == 6));
        }

        private bool IsGotoOper(int op)
        {
            return (((((op == this.OP_GO) || (op == this.OP_GO_FALSE)) || ((op == this.OP_GO_TRUE) || (op == this.OP_GO_NULL))) || (op == this.OP_GOTO_START)) || (op == this.OP_GOTO_CONTINUE));
        }

        public static bool IsIntegralTypeId(int id)
        {
            return (((((id == 10) || (id == 3)) || ((id == 11) || (id == 15))) || (((id == 8) || (id == 13)) || ((id == 9) || (id == 14)))) || (id == 4));
        }

        public static bool IsNumericTypeId(int id)
        {
            return ((IsIntegralTypeId(id) || IsFloatingPointTypeId(id)) || (id == 5));
        }

        public static bool IsSimpleTypeId(int id)
        {
            return (IsNumericTypeId(id) || (id == 2));
        }

        public void LinkGoTo()
        {
            this.LinkGoToEx(0);
        }

        public void LinkGoToEx(int init_n)
        {
            for (int i = init_n + 1; i <= this.Card; i++)
            {
                ProgRec rec = (ProgRec) this.prog[i];
                if (((rec.op == this.OP_GO) || (rec.op == this.OP_GOTO_START)) || (((rec.op == this.OP_GO_FALSE) || (rec.op == this.OP_GO_TRUE)) || (rec.op == this.OP_GO_NULL)))
                {
                    if (this.GetVal(rec.arg1) == null)
                    {
                        string name = this.symbol_table[rec.arg1].Name;
                        int level = this.symbol_table[rec.arg1].Level;
                        int id = this.symbol_table.LookupID(name, level, true);
                        if (id == 0)
                        {
                            this.scripter.CreateErrorObjectEx("CS0159. No such label '{0}' within the scope of the goto statement.", new object[] { name });
                        }
                        if (this.GetVal(id) == null)
                        {
                            this.scripter.CreateErrorObjectEx("CS0159. No such label '{0}' within the scope of the goto statement.", new object[] { name });
                        }
                    }
                    ProgRec codeProgRec = this.symbol_table[rec.arg1].CodeProgRec;
                    rec.arg1 = codeProgRec.FinalNumber;
                }
            }
        }

        public void LoadFromStream(BinaryReader br,  Module m, int ds, int dp)
        {
            bool flag = (ds != 0) || (dp != 0);
            for (int i = m.P1; i <= m.P2; i++)
            {
                this.Card++;
                this.r = this[this.Card];
                this.r.LoadFromStream(br);
                if ((this.r.op != this.OP_SEPARATOR) && flag)
                {
                    if (m.IsInternalId(this.r.arg1))
                    {
                        this.r.arg1 += ds;
                    }
                    if (m.IsInternalId(this.r.arg2))
                    {
                        this.r.arg2 += ds;
                    }
                    if (m.IsInternalId(this.r.res))
                    {
                        this.r.res += ds;
                    }
                }
            }
        }

        private ProgRec NextInstruction(int j)
        {
            j++;
            while (this[j].op == this.OP_SEPARATOR)
            {
                j++;
            }
            return this[j];
        }

        public int NextLine()
        {
            if ((this.n > this.Card) || (this.n < 1))
            {
                return -1;
            }
            int n = this.n;
            while (this[n].op != this.OP_SEPARATOR)
            {
                if (n > this.Card)
                {
                    return -1;
                }
                n++;
            }
            return n;
        }

        private void OperAddAddAccessor()
        {
            int id = this.r.arg1;
            this.GetEventObject(id).AddId = this.r.arg2;
            this.n++;
        }

        private void OperAddDefaultValue()
        {
            this.GetFunctionObject(this.r.arg1).AddDefaultValueId(this.r.arg2, this.r.res);
            this.n++;
        }

        private void OperAddDelegates()
        {
            if (this.GetValue(this.r.arg1) == null)
            {
                this.PutVal(this.r.res, this.GetObjectObject(this.r.arg2));
            }
            else
            {
                object obj7;
                FunctionObject obj8;
                bool flag;
                ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
                ObjectObject obj4 = this.GetObjectObject(this.r.arg2);
                ObjectObject obj6 = objectObject.Class_Object.CreateObject();
                for (flag = objectObject.FindFirstInvocation(out obj7, out obj8); flag; flag = objectObject.FindNextInvocation(out obj7, out obj8))
                {
                    obj6.AddInvocation(obj7, obj8);
                }
                for (flag = obj4.FindFirstInvocation(out obj7, out obj8); flag; flag = obj4.FindNextInvocation(out obj7, out obj8))
                {
                    obj6.AddInvocation(obj7, obj8);
                }
                this.PutVal(this.r.res, obj6);
            }
            this.n++;
        }

        private void OperAddEventField()
        {
            this.GetEventObject(this.r.arg1).EventFieldId = this.r.arg2;
            this.n++;
        }

        private void OperAddExplicitInterface()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            ClassObject classObject = this.GetClassObject(this.r.arg2);
            functionObject.ExplicitInterface = classObject;
            this.n++;
        }

        private void OperAddIndex()
        {
            IndexObject indexObject = this.GetIndexObject(this.r.arg1);
            object v = this.GetValue(this.r.arg2);
            indexObject.AddIndex(v);
            this.n++;
        }

        private void OperAdditionDecimal()
        {
            this.PutValueAsDecimal(this.r.res, this.GetValueAsDecimal(this.r.arg1) + this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperAdditionDouble()
        {
            this.PutValueAsDouble(this.r.res, this.GetValueAsDouble(this.r.arg1) + this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperAdditionFloat()
        {
            this.PutValueAsFloat(this.r.res, this.GetValueAsFloat(this.r.arg1) + this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperAdditionInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) + this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperAdditionLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) + this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperAdditionString()
        {
            this.PutValueAsString(this.r.res, this.GetValueAsString(this.r.arg1) + this.GetValueAsString(this.r.arg2));
            this.n++;
        }

        private void OperAdditionUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) + this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperAdditionUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) + this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperAddMaxValue()
        {
            this.GetClassObject(this.r.arg1).MaxValueId = this.r.arg2;
            this.n++;
        }

        private void OperAddMinValue()
        {
            this.GetClassObject(this.r.arg1).MinValueId = this.r.arg2;
            this.n++;
        }

        private void OperAddModifier()
        {
            int id = this.r.arg1;
            Modifier modifier = (Modifier) this.r.arg2;
            this.GetMemberObject(id).AddModifier(modifier);
            int level = this.symbol_table[id].Level;
            ClassObject classObject = this.GetClassObject(level);
            this.n++;
        }

        private void OperAddParam()
        {
            this.GetFunctionObject(this.r.arg1).AddParam(this.r.arg2, (ParamMod) this.r.res);
            this.n++;
        }

        private void OperAddParams()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            functionObject.AddParam(this.r.arg2, (ParamMod) this.r.res);
            functionObject.ParamsId = this.r.arg2;
            int typeId = this.symbol_table[functionObject.ParamsId].TypeId;
            string elementTypeName = CSLite_System.GetElementTypeName(this.symbol_table[typeId].Name);
            int num2 = this.scripter.GetTypeId(elementTypeName);
            functionObject.ParamsElementId = this.AppVar(0, num2);
            this.n++;
        }

        private void OperAddPattern()
        {
            this.GetClassObject(this.r.arg1).PatternMethod = this.GetFunctionObject(this.r.arg2);
            this.n++;
        }

        private void OperAddReadAccessor()
        {
            int id = this.r.arg1;
            this.GetPropertyObject(id).ReadId = this.r.arg2;
            this.n++;
        }

        private void OperAddRemoveAccessor()
        {
            int id = this.r.arg1;
            this.GetEventObject(id).RemoveId = this.r.arg2;
            this.n++;
        }

        private void OperAddWriteAccessor()
        {
            int id = this.r.arg1;
            this.GetPropertyObject(id).WriteId = this.r.arg2;
            this.n++;
        }

        private void OperAs()
        {
            object v = this.GetValue(this.r.arg1);
            if (v == null)
            {
                this.PutValue(this.r.res, null);
            }
            else
            {
                ObjectObject obj3 = this.scripter.ToScriptObject(v);
                if (obj3.Class_Object.Id == this.r.arg2)
                {
                    this.PutValue(this.r.res, v);
                }
                else if (obj3.Class_Object.IsValueType)
                {
                    ClassObject obj4 = obj3.Class_Object;
                    ClassObject classObject = this.GetClassObject(this.r.arg2);
                    if (classObject.Class_Kind == ClassKind.Interface)
                    {
                        if (obj4.Implements(classObject))
                        {
                            this.PutVal(this.r.res, v);
                        }
                        else
                        {
                            this.PutValue(this.r.res, null);
                        }
                    }
                    else
                    {
                        this.PutValue(this.r.res, null);
                    }
                }
                else
                {
                    ClassObject a = obj3.Class_Object;
                    if (this.GetClassObject(this.r.arg2).InheritsFrom(a))
                    {
                        this.PutVal(this.r.res, v);
                    }
                    else
                    {
                        this.PutValue(this.r.res, null);
                    }
                }
            }
            this.n++;
        }

        private void OperAssign()
        {
            object obj2 = null;
            if (this.symbol_table[this.r.res].is_static)
            {
                ProgRec rec = this[this.n + 1];
                if ((rec.op == this.OP_INIT_STATIC_VAR) && (rec.arg1 == this.r.res))
                {
                    if (rec.res == 0)
                    {
                        rec.res = 1;
                        obj2 = this.GetValue(this.r.arg2);
                        this.PutValue(this.r.res, obj2);
                    }
                }
                else
                {
                    obj2 = this.GetValue(this.r.arg2);
                    this.PutValue(this.r.res, obj2);
                }
            }
            else
            {
                obj2 = this.GetValue(this.r.arg2);
                this.PutValue(this.r.res, obj2);
            }
            this.n++;
        }

        private void OperAssignStruct()
        {
            this.PutValue(this.r.res, this.scripter.ToScriptObject(this.GetValue(this.r.arg2)).Clone());
            this.n++;
        }

        private void OperBitwiseAndBool()
        {
            this.PutValue(this.r.res, this.GetValueAsBool(this.r.arg1) & this.GetValueAsBool(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseAndInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) & this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseAndLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) & this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseAndUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) & this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperBitwiseAndUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) & this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperBitwiseComplementInt()
        {
            this.PutValue(this.r.res, ~this.GetValueAsInt(this.r.arg1));
            this.n++;
        }

        private void OperBitwiseComplementLong()
        {
            this.PutValueAsLong(this.r.res, ~this.GetValueAsLong(this.r.arg1));
            this.n++;
        }

        private void OperBitwiseComplementUint()
        {
            this.PutValue(this.r.res, (uint) ~this.GetValueAsInt(this.r.arg1));
            this.n++;
        }

        private void OperBitwiseComplementUlong()
        {
            this.PutValue(this.r.res, (ulong) ~this.GetValueAsLong(this.r.arg1));
            this.n++;
        }

        private void OperBitwiseOrBool()
        {
            this.PutValue(this.r.res, this.GetValueAsBool(this.r.arg1) | this.GetValueAsBool(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseOrInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) | this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseOrLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) | this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseOrUint()
        {
            this.PutValue(this.r.res, ((uint) this.GetValue(this.r.arg1)) | ((uint) this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperBitwiseOrUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) | this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperBitwiseXorBool()
        {
            this.PutValue(this.r.res, this.GetValueAsBool(this.r.arg1) ^ this.GetValueAsBool(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseXorInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) ^ this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseXorLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) ^ this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperBitwiseXorUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) ^ this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperBitwiseXorUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) ^ this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperCall()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            if (functionObject.Imported)
            {
                this.OperHostCall();
            }
            else
            {
                object obj3 = this.stack.Pop();
                functionObject.AllocateSub();
                functionObject.PutThis(obj3);
                CallStackRec csr = null;
                if (this.debugging)
                {
                    csr = new CallStackRec(this.scripter, functionObject, this.n);
                }
                int num = this.r.arg2;
                for (int i = 0; i < num; i++)
                {
                    obj3 = this.stack.Pop();
                    functionObject.PutParam((num - 1) - i, obj3);
                    if (this.debugging)
                    {
                        csr.Parameters.Insert(0, obj3);
                    }
                }
                if (this.debugging)
                {
                    this.callstack.Add(csr);
                }
                this.stack.Push(this.n);
                this.n = functionObject.Init.FinalNumber;
                this.curr_stack_count = this.stack.Count;
            }
        }

        private void OperCallAddEvent()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            if (!functionObject.Imported)
            {
                this.OperCall();
            }
            else
            {
                object instance = this.stack.Pop();
                if (instance.GetType() == typeof(ObjectObject))
                {
                    instance = (instance as ObjectObject).Instance;
                }
                object obj4 = this.stack.Pop();
                int typeId = this.symbol_table[functionObject.Param_Ids[0]].TypeId;
                ClassObject classObject = this.GetClassObject(typeId);
                if (classObject.IsDelegate)
                {
                    string name = functionObject.Name.Substring(4);
                    EventInfo e = instance.GetType().GetEvent(name);
                    FunctionObject patternMethod = classObject.PatternMethod;
                    if ((e != null) && (patternMethod != null))
                    {
                        Delegate handler = this.scripter.CreateDelegate(instance, e, patternMethod, obj4);
                        e.AddEventHandler(instance, handler);
                    }
                }
                this.n++;
            }
        }

        private void OperCallBase()
        {
            this.OperCall();
        }

        private void OperCallVirt()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            object v = this.stack.Pop();
            int typeId = this.symbol_table[this[this.n - 1].arg1].TypeId;
            if (this.GetClassObject(typeId).IsInterface)
            {
                typeId = this.scripter.ToScriptObject(v).Class_Object.Id;
            }
            functionObject = functionObject.GetLateBindingFunction(v, typeId, this.GetUpcase());
            if (functionObject.Imported)
            {
                this.stack.Push(v);
                this.OperHostCall();
            }
            else
            {
                functionObject.AllocateSub();
                functionObject.PutThis(v);
                CallStackRec csr = null;
                if (this.debugging)
                {
                    csr = new CallStackRec(this.scripter, functionObject, this.n);
                }
                int num2 = this.r.arg2;
                for (int i = 0; i < num2; i++)
                {
                    v = this.stack.Pop();
                    functionObject.PutParam((num2 - 1) - i, v);
                    if (this.debugging)
                    {
                        csr.Parameters.Insert(0, v);
                    }
                }
                if (this.debugging)
                {
                    this.callstack.Add(csr);
                }
                this.stack.Push(this.n);
                this.n = functionObject.Init.FinalNumber;
            }
        }

        private void OperCast()
        {
            object obj2 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, obj2);
            this.n++;
        }

        private void OperCatch()
        {
            this.n++;
        }

        private void OperChecked()
        {
            this.Checked = (bool) this.GetVal(this.r.arg1);
            this.SaveCheckedState();
            this.n++;
        }

        private void OperCreateArrayInstance()
        {
            Type importedType;
            ClassObject classObject = this.GetClassObject(this.r.arg1);
            object v = this.stack.Pop();
            int num = this.r.arg2;
            int[] lengths = new int[num];
            for (int i = 0; i < num; i++)
            {
                object obj4 = this.stack.Pop();
                if (obj4 == null)
                {
                    lengths[(num - 1) - i] = 0;
                }
                else if (obj4.GetType() == typeof(ObjectObject))
                {
                    ObjectObject obj5 = obj4 as ObjectObject;
                    lengths[(num - 1) - i] = ConvertHelper.ToInt(obj5.Instance);
                }
                else
                {
                    lengths[(num - 1) - i] = ConvertHelper.ToInt(obj4);
                }
            }
            if (classObject.ImportedType != null)
            {
                importedType = classObject.ImportedType;
            }
            else
            {
                importedType = typeof(object);
            }
            this.scripter.ToScriptObject(v).Instance = Array.CreateInstance(importedType, lengths);
            this.n++;
        }

        private void OperCreateClass()
        {
            int num = this.r.arg1;
            int id = this.r.arg2;
            ClassObject classObject = this.GetClassObject(id);
            if (this.scripter.HasPredefinedNamespace(this.symbol_table[num].FullName))
            {
                this.scripter.CreateErrorObjectEx("CS0519. '{0}' conflicts with a predefined namespace.", new object[] { this.symbol_table[num].FullName });
            }
            else
            {
                bool upcase = this.GetUpcase();
                int nameIndex = this.symbol_table[num].NameIndex;
                if (classObject.GetMemberByNameIndex(nameIndex, upcase) != null)
                {
                    string name = this.symbol_table[num].Name;
                    if (classObject.Class_Kind == ClassKind.Namespace)
                    {
                        this.scripter.CreateErrorObjectEx("CS0101. The namespace '{0}' already contains a definition for '{1}'.", new object[] { classObject.Name, name });
                    }
                    else
                    {
                        this.scripter.CreateErrorObjectEx("CS0102. The class '{0}' already contains a definition for '{1}'.", new object[] { classObject.Name, name });
                    }
                }
                else
                {
                    ClassKind res = (ClassKind) this.r.res;
                    ClassObject obj4 = new ClassObject(this.scripter, num, id, res);
                    obj4.PCodeLine = this.n;
                    this.PutVal(num, obj4);
                    classObject.AddMember(obj4);
                    this.n++;
                }
            }
        }

        private void OperCreateEvent()
        {
            int num = this.r.arg1;
            int id = this.r.arg2;
            int res = this.r.res;
            ClassObject classObject = this.GetClassObject(id);
            bool upcase = this.GetUpcase();
            int nameIndex = this.symbol_table[num].NameIndex;
            if (classObject.GetMemberByNameIndex(nameIndex, upcase) != null)
            {
                string name = this.symbol_table[num].Name;
                this.scripter.CreateErrorObjectEx("CS0102. The class '{0}' already contains a definition for '{1}'.", new object[] { classObject.Name, name });
            }
            else
            {
                EventObject obj4 = new EventObject(this.scripter, num, id);
                obj4.PCodeLine = this.n;
                this.PutVal(num, obj4);
                classObject.AddMember(obj4);
                this.n++;
            }
        }

        private void OperCreateField()
        {
            int num = this.r.arg1;
            int id = this.r.arg2;
            ClassObject classObject = this.GetClassObject(id);
            bool upcase = this.GetUpcase();
            int nameIndex = this.symbol_table[num].NameIndex;
            if (classObject.GetMemberByNameIndex(nameIndex, upcase) != null)
            {
                string name = this.symbol_table[num].Name;
                this.scripter.CreateErrorObjectEx("CS0102. The class '{0}' already contains a definition for '{1}'.", new object[] { classObject.Name, name });
            }
            else
            {
                FieldObject obj4 = new FieldObject(this.scripter, num, id);
                obj4.PCodeLine = this.n;
                this.PutVal(num, obj4);
                classObject.AddMember(obj4);
                this.n++;
            }
        }

        private void OperCreateIndexObject()
        {
            ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
            IndexObject obj3 = new IndexObject(this.scripter, objectObject);
            this.PutVal(this.r.res, obj3);
            this.n++;
            ClassObject obj4 = objectObject.Class_Object;
            if (obj4.IsPascalArray)
            {
                ClassObject classObject = this.GetClassObject(obj4.RangeTypeId);
                obj3.MinValue = classObject.MinValue;
            }
        }

        private void OperCreateMethod()
        {
            int num = this.r.arg1;
            int num2 = this.r.arg2;
            FunctionObject obj2 = new FunctionObject(this.scripter, num, num2);
            this.PutVal(num, obj2);
            this.GetClassObject(num2).AddMember(obj2);
            this.n++;
        }

        private void OperCreateNamespace()
        {
            int num = this.r.arg1;
            int id = this.r.arg2;
            string fullName = this.symbol_table[num].FullName;
            bool upcase = this.GetUpcase();
            if (this.scripter.FindAvailableType(fullName, upcase) != null)
            {
                this.scripter.CreateErrorObjectEx("CS0010. Cannot declare a namespace and a type both named '{0}'.", new object[] { fullName });
            }
            else
            {
                ClassObject classObject = this.GetClassObject(id);
                if (classObject != null)
                {
                    int nameIndex = this.symbol_table[num].NameIndex;
                    MemberObject memberByNameIndex = classObject.GetMemberByNameIndex(nameIndex, upcase);
                    if (memberByNameIndex != null)
                    {
                        if (memberByNameIndex.Kind == MemberKind.Type)
                        {
                            ClassObject obj4 = memberByNameIndex as ClassObject;
                            if (obj4.Class_Kind == ClassKind.Namespace)
                            {
                                this.symbol_table[num].Kind = MemberKind.None;
                                this.ReplaceId(num, memberByNameIndex.Id);
                                return;
                            }
                        }
                        string name = this.symbol_table[num].Name;
                        this.scripter.CreateErrorObjectEx("CS0101. The namespace '{0}' already contains a definition for '{1}'.", new object[] { classObject.Name, name });
                        return;
                    }
                }
                ClassKind res = (ClassKind) this.r.res;
                ClassObject obj5 = new ClassObject(this.scripter, num, id, res);
                obj5.PCodeLine = this.n;
                this.PutVal(num, obj5);
                if (classObject != null)
                {
                    classObject.AddMember(obj5);
                }
                this.n++;
            }
        }

        private void OperCreateObject()
        {
            int id = this.r.arg1;
            int res = this.r.res;
            ClassObject classObject = this.GetClassObject(id);
            ObjectObject obj3 = classObject.CreateObject();
            this.PutValue(res, obj3);
            if (classObject.IsPascalArray)
            {
                Type importedType = classObject.ImportedType;
                ClassObject obj4 = this.GetClassObject(classObject.RangeTypeId);
                int minValue = obj4.MinValue;
                int maxValue = obj4.MaxValue;
                Array array = Array.CreateInstance(importedType, (int) ((maxValue - minValue) + 1));
                obj3.Instance = array;
                ClassObject obj5 = this.GetClassObject(classObject.IndexTypeId);
                if (!obj5.Imported)
                {
                    for (int i = minValue; i <= maxValue; i++)
                    {
                        object obj6 = obj5.CreateObject();
                        array.SetValue(obj6, (int) (i - minValue));
                    }
                }
            }
            this.n++;
        }

        private void OperCreateProperty()
        {
            int num = this.r.arg1;
            int id = this.r.arg2;
            int res = this.r.res;
            ClassObject classObject = this.GetClassObject(id);
            PropertyObject obj3 = new PropertyObject(this.scripter, num, id, res);
            obj3.PCodeLine = this.n;
            this.PutVal(num, obj3);
            classObject.AddMember(obj3);
            this.n++;
        }

        private void OperCreateReference()
        {
            this.PutVal(this.r.res, this.GetObjectObject(this.r.arg1));
            this.n++;
        }

        private void OperCreateUsingAlias()
        {
            int nameIndex = this.symbol_table[this.r.arg1].NameIndex;
            bool upcase = this.GetUpcase();
            ClassObject classObject = this.GetClassObject(this.r.arg2);
            MemberObject memberByNameIndex = classObject.GetMemberByNameIndex(nameIndex, upcase);
            if (memberByNameIndex == null)
            {
                memberByNameIndex = new MemberObject(this.scripter, this.r.arg1, classObject.Id);
                memberByNameIndex.PCodeLine = this.n;
                memberByNameIndex.Kind = MemberKind.Alias;
                classObject.AddMember(memberByNameIndex);
                this.PutVal(memberByNameIndex.Id, memberByNameIndex);
            }
            else
            {
                this.scripter.CreateErrorObjectEx("CS1537. The using alias 'alias' appeared previously in this namespace.", new object[] { memberByNameIndex.Name });
                return;
            }
            this.symbol_table[this.r.arg1].Kind = MemberKind.Alias;
            this.n++;
        }

        private void OperDecByte()
        {
            byte valueAsByte = this.GetValueAsByte(this.r.arg1);
            this.PutValue(this.r.res, valueAsByte = (byte) (valueAsByte - 1));
            this.n++;
        }

        private void OperDecChar()
        {
            char valueAsChar = this.GetValueAsChar(this.r.arg1);
            this.PutValue(this.r.res, valueAsChar = (char) (valueAsChar - '\x0001'));
            this.n++;
        }

        private void OperDecDecimal()
        {
            decimal valueAsDecimal = this.GetValueAsDecimal(this.r.arg1);
            this.PutValueAsDecimal(this.r.res, --valueAsDecimal);////valueAsDecimal = decimal_op_Decrement(valueAsDecimal));
            this.n++;
        }

        private decimal decimal_op_Decrement(decimal valueAsDecimal) {
            return valueAsDecimal--;
        }

        private void OperDecDouble()
        {
            double valueAsDouble = this.GetValueAsDouble(this.r.arg1);
            this.PutValueAsDouble(this.r.res, --valueAsDouble);
            this.n++;
        }

        private void OperDecFloat()
        {
            float valueAsFloat = this.GetValueAsFloat(this.r.arg1);
            this.PutValueAsFloat(this.r.res, --valueAsFloat);
            this.n++;
        }

        private void OperDecInt()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg1);
            this.PutValueAsInt(this.r.res, --valueAsInt);
            this.n++;
        }

        private void OperDeclareLocalVariable()
        {
            int id = this.r.arg1;
            int num2 = this.r.arg2;
            this.r.op = this.OP_DECLARE_LOCAL_VARIABLE_RUNTIME;
            bool flag = false;
            bool flag2 = false;
            if (this.symbol_table[id].Kind == MemberKind.Label)
            {
                flag2 = true;
            }
            int typeId = this.GetTypeId(id);
            if (this.GetClassObject(typeId).IsStruct)
            {
                flag = true;
            }
            int n = this.n;
        Label_006F:
            n++;
            int op = this[n].op;
            if (op == this.OP_SEPARATOR)
            {
                goto Label_006F;
            }
            if (op != this.OP_END_METHOD)
            {
                bool flag3 = false;
                flag3 |= op == this.OP_ASSIGN;
                flag3 |= op == this.OP_ASSIGN_STRUCT;
                flag3 |= op == this.OP_PUSH;
                flag3 |= op == this.OP_CALL;
                flag3 |= op == this.OP_CALL_VIRT;
                flag3 |= op == this.OP_CREATE_OBJECT;
                flag3 |= op == this.OP_CALL_SIMPLE;
                flag3 |= op == this.OP_CALL_BASE;
                flag3 |= op == this.OP_CREATE_REFERENCE;
                flag3 |= op == this.OP_CAST;
                flag3 |= op == this.OP_GO_FALSE;
                flag3 |= op == this.OP_GO_TRUE;
                flag3 |= this.overloadable_binary_operators_str[op] != null;
                if (!(flag3 | (this.overloadable_unary_operators_str[op] != null)))
                {
                    goto Label_006F;
                }
                if ((op == this.OP_ASSIGN) && (this[n].arg1 == id))
                {
                    flag2 = true;
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_ASSIGN_STRUCT) && (this[n].arg1 == id))
                {
                    flag2 = true;
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_CALL) && (this[n].res == id))
                {
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_CALL_VIRT) && (this[n].res == id))
                {
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_CALL_SIMPLE) && (this[n].res == id))
                {
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_CALL_BASE) && (this[n].res == id))
                {
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_CREATE_OBJECT) && (this[n].res == id))
                {
                    flag = true;
                    goto Label_006F;
                }
                if ((op == this.OP_PUSH) && (this[n].arg1 == id))
                {
                    flag2 = true;
                    if (this[n].arg2 == 2)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        goto Label_006F;
                    }
                    int num6 = this.n;
                    this.n = n;
                    this.scripter.CreateErrorObjectEx("CS0165. Use of unassigned local variable '{0}'.", new object[] { this.symbol_table[id].Name });
                    this.n = num6;
                }
                else
                {
                    if ((op == this.OP_CREATE_REFERENCE) && (this[n].arg1 == id))
                    {
                        flag2 = true;
                        goto Label_006F;
                    }
                    if ((this[n].arg2 != id) && (this[n].res != id))
                    {
                        goto Label_006F;
                    }
                    flag2 = true;
                    if (flag)
                    {
                        goto Label_006F;
                    }
                    int num7 = this.n;
                    this.n = n;
                    this.scripter.CreateErrorObjectEx("CS0165. Use of unassigned local variable '{0}'.", new object[] { this.symbol_table[id].Name });
                    this.n = num7;
                }
            }
            if (!flag2)
            {
                n = this.n;
                do
                {
                    n++;
                    if (this[n].arg1 == id)
                    {
                        flag2 = true;
                    }
                    if (this[n].arg2 == id)
                    {
                        flag2 = true;
                    }
                    if (this[n].res == id)
                    {
                        flag2 = true;
                    }
                }
                while (this[n].op != this.OP_END_METHOD);
                if (flag)
                {
                    if (!flag2)
                    {
                        this.scripter.CreateWarningObjectEx("CS0219. The variable '{0}' is assigned but its value is never used.", new object[] { this.symbol_table[id].Name });
                    }
                }
                else if (!flag2)
                {
                    this.scripter.CreateWarningObjectEx("CS0168. The variable '{0}' is declared but never used.", new object[] { this.symbol_table[id].Name });
                }
            }
            this.n++;
        }

        private void OperDecLong()
        {
            long valueAsLong = this.GetValueAsLong(this.r.arg1);
            this.PutValueAsLong(this.r.res, valueAsLong -= 1L);
            this.n++;
        }

        private void OperDecSbyte()
        {
            sbyte num = (sbyte) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(sbyte));
            this.PutValue(this.r.res, num = (sbyte) (num - 1));
            this.n++;
        }

        private void OperDecShort()
        {
            short valueAsShort = this.GetValueAsShort(this.r.arg1);
            this.PutValue(this.r.res, valueAsShort = (short) (valueAsShort - 1));
            this.n++;
        }

        private void OperDecUint()
        {
            uint num = (uint) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(uint));
            this.PutValue(this.r.res, --num);
            this.n++;
        }

        private void OperDecUlong()
        {
            ulong num = (ulong) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(ulong));
            this.PutValue(this.r.res, num -= (ulong) 1L);
            this.n++;
        }

        private void OperDecUshort()
        {
            ushort num = (ushort) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(ushort));
            this.PutValue(this.r.res, num = (ushort) (num - 1));
            this.n++;
        }

        private void OperDiscardError()
        {
            this.scripter.DiscardError();
            this.n++;
        }

        private void OperDispose()
        {
            ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
            if (objectObject.Instance != null)
            {
                ((IDisposable) objectObject.Instance).Dispose();
            }
            this.n++;
        }

        private void OperDiv()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) / ((int) obj3));
            this.n++;
        }

        private void OperDivisionDecimal()
        {
            this.PutValueAsDecimal(this.r.res, this.GetValueAsDecimal(this.r.arg1) / this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperDivisionDouble()
        {
            this.PutValueAsDouble(this.r.res, this.GetValueAsDouble(this.r.arg1) / this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperDivisionFloat()
        {
            this.PutValueAsFloat(this.r.res, this.GetValueAsFloat(this.r.arg1) / this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperDivisionInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) / this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperDivisionLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) / this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperDivisionUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) / this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperDivisionUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) / this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperDynamicInvoke()
        {
            bool terminated = this.Terminated;
            Delegate delegate2 = this.GetValue(this.r.arg1) as Delegate;
            object obj2 = this.stack.Pop();
            int num = this.r.arg2;
            object[] args = new object[num];
            for (int i = 0; i < num; i++)
            {
                object obj3 = this.stack.Pop();
                if (obj3 == null)
                {
                    args[(num - 1) - i] = obj3;
                }
                else if (obj3.GetType() == typeof(ObjectObject))
                {
                    ObjectObject obj4 = obj3 as ObjectObject;
                    args[(num - 1) - i] = obj4.Instance;
                }
                else
                {
                    args[(num - 1) - i] = obj3;
                }
            }
            object obj5 = delegate2.DynamicInvoke(args);
            if (this.r.res != 0)
            {
                this.PutValue(this.r.res, obj5);
            }
            this.Terminated = terminated;
            this.n++;
        }

        private void OperEndMethod()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            functionObject.SetupLowBound(this.r.arg2);
            functionObject.SetupParameters();
            this.n++;
        }

        private void OperEq()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, obj2 == obj3);
            this.n++;
        }

        private void OperEqDelegates()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            if ((obj2 == null) || (obj3 == null))
            {
                this.PutVal(this.r.res, obj2 == obj3);
            }
            else
            {
                ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
                ObjectObject obj5 = this.GetObjectObject(this.r.arg2);
                this.PutVal(this.r.res, this.CompareDelegates(objectObject, obj5));
            }
            this.n++;
        }

        private void OperEqualityBool()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsBool(this.r.arg1) == this.GetValueAsBool(this.r.arg2));
            this.n++;
        }

        private void OperEqualityDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) == this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperEqualityDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) == this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperEqualityFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) == this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperEqualityInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) == this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperEqualityLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) == this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperEqualityObject()
        {
            this.PutValueAsBool(this.r.res, this.GetValue(this.r.arg1) == this.GetValue(this.r.arg2));
            this.n++;
        }

        private void OperEqualityString()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsString(this.r.arg1) == this.GetValueAsString(this.r.arg2));
            this.n++;
        }

        private void OperEqualityUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) == this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperEqualityUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) == this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperExitOnError()
        {
            if (this.scripter.IsError())
            {
                this.RaiseError();
            }
            else
            {
                this.n++;
            }
        }

        private void OperExitSub()
        {
            do
            {
                this.n++;
                this.r = (ProgRec) this.prog[this.n];
            }
            while (this.r.op != this.OP_RET);
        }

        private void OperExp()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) * ((int) obj3));
            this.n++;
        }

        private void OperExponentDecimal()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            decimal valueAsDecimal = this.GetValueAsDecimal(this.r.arg1);
            decimal num3 = valueAsDecimal;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= valueAsDecimal;
            }
            this.PutValueAsDecimal(this.r.res, num3);
            this.n++;
        }

        private void OperExponentDouble()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            double valueAsDouble = this.GetValueAsDouble(this.r.arg1);
            double num3 = valueAsDouble;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= valueAsDouble;
            }
            this.PutValueAsDouble(this.r.res, num3);
            this.n++;
        }

        private void OperExponentFloat()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            float valueAsFloat = this.GetValueAsFloat(this.r.arg1);
            float num3 = valueAsFloat;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= valueAsFloat;
            }
            this.PutValueAsFloat(this.r.res, num3);
            this.n++;
        }

        private void OperExponentInt()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            int num2 = this.GetValueAsInt(this.r.arg1);
            int num3 = num2;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= num2;
            }
            this.PutValueAsInt(this.r.res, num3);
            this.n++;
        }

        private void OperExponentLong()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            long valueAsLong = this.GetValueAsLong(this.r.arg1);
            long num3 = valueAsLong;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= valueAsLong;
            }
            this.PutValueAsLong(this.r.res, num3);
            this.n++;
        }

        private void OperExponentUint()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            uint num2 = (uint) this.GetValueAsInt(this.r.arg1);
            uint num3 = num2;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= num2;
            }
            this.PutValue(this.r.res, num3);
            this.n++;
        }

        private void OperExponentUlong()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg2);
            ulong valueAsLong = (ulong) this.GetValueAsLong(this.r.arg1);
            ulong num3 = valueAsLong;
            for (int i = 1; i <= (valueAsInt - 1); i++)
            {
                num3 *= valueAsLong;
            }
            this.PutValue(this.r.res, num3);
            this.n++;
        }

        private void OperFinally()
        {
            this.n++;
        }

        private void OperFindFirstDelegate()
        {
            object obj3;
            FunctionObject obj4;
            this.GetObjectObject(this.r.arg1).FindFirstInvocation(out obj3, out obj4);
            this.PutVal(this.r.arg2, obj4);
            this.PutVal(this.r.res, obj3);
            this.n++;
        }

        private void OperFindNextDelegate()
        {
            object obj3;
            FunctionObject obj4;
            this.GetObjectObject(this.r.arg1).FindNextInvocation(out obj3, out obj4);
            this.PutVal(this.r.arg2, obj4);
            this.PutVal(this.r.res, obj3);
            this.n++;
        }

        private void OperGe()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) >= ((int) obj3));
            this.n++;
        }

        private void OperGetParamValue()
        {
            object paramValue = this.GetFunctionObject(this.r.arg1).GetParamValue(this.r.arg2);
            this.PutVal(this.r.res, paramValue);
            this.n++;
        }

        private void OperGo()
        {
            this.n = this.r.arg1;
        }

        private void OperGoFalse()
        {
            if (this.GetValueAsBool(this.r.arg2))
            {
                this.n++;
            }
            else
            {
                this.n = this.r.arg1;
            }
        }

        private void OperGoNull()
        {
            if (this.GetValue(this.r.arg2) == null)
            {
                this.n = this.r.arg1;
            }
            else
            {
                this.n++;
            }
        }

        private void OperGotoContinue()
        {
            if (this.goto_line == 0)
            {
                this.n++;
                return;
            }
        Label_0020:
            this.r = (ProgRec) this.prog[this.n];
            int op = this.r.op;
            if (this.n == this.goto_line)
            {
                this.goto_line = 0;
            }
            else
            {
                if (op == this.OP_FINALLY)
                {
                    if (this.try_stack.Legal(this.n))
                    {
                        return;
                    }
                }
                else
                {
                    if ((op == this.OP_RET) || (op == this.OP_HALT))
                    {
                        if (this.goto_line > 0)
                        {
                            this.n = this.goto_line;
                            this.goto_line = 0;
                        }
                        return;
                    }
                    this.n++;
                }
                goto Label_0020;
            }
        }

        private void OperGotoStart()
        {
            this.goto_line = this.r.arg1;
            this.OperGotoContinue();
        }

        private void OperGoTrue()
        {
            if (this.GetValueAsBool(this.r.arg2))
            {
                this.n = this.r.arg1;
            }
            else
            {
                this.n++;
            }
        }

        private void OperGreaterThanDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) > this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) > this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) > this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) > this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) > this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) >= this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) >= this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) >= this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) >= this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) >= this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualString()
        {
            int num = string.Compare(this.GetValueAsString(this.r.arg1), this.GetValueAsString(this.r.arg2));
            this.PutValueAsBool(this.r.res, num >= 0);
            this.n++;
        }

        private void OperGreaterThanOrEqualUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) >= this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanOrEqualUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) >= this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanString()
        {
            int num = string.Compare(this.GetValueAsString(this.r.arg1), this.GetValueAsString(this.r.arg2));
            this.PutValueAsBool(this.r.res, num > 0);
            this.n++;
        }

        private void OperGreaterThanUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) > this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperGreaterThanUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) > this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperGt()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) > ((int) obj3));
            this.n++;
        }

        private void OperHalt()
        {
            this.Terminated = true;
        }

        private void OperHostCall()
        {
            bool terminated = this.Terminated;
            ScripterState state = this.scripter.Owner.State;
            this.scripter.Owner.State = ScripterState.Running;
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            object v = this.stack.Pop();
            CallStackRec csr = null;
            if (this.debugging)
            {
                csr = new CallStackRec(this.scripter, functionObject, this.n);
            }
            int paramCount = functionObject.ParamCount;
            for (int i = 0; i < paramCount; i++)
            {
                object obj4 = this.stack.Pop();
                if (this.debugging)
                {
                    csr.Parameters.Insert(0, obj4);
                }
                if (obj4 == null)
                {
                    functionObject.Params[(paramCount - 1) - i] = obj4;
                }
                else if (obj4.GetType() == typeof(ObjectObject))
                {
                    ObjectObject obj5 = obj4 as ObjectObject;
                    int num3 = functionObject.Param_Ids[i];
                    if ((this.symbol_table[num3].TypeId == 0x10) && ((functionObject.Owner.NamespaceNameIndex == this.symbol_table.SYSTEM_COLLECTIONS_ID) || (functionObject.Owner.Name == "ArrayList")))
                    {
                        functionObject.Params[(paramCount - 1) - i] = obj5;
                    }
                    else
                    {
                        functionObject.Params[(paramCount - 1) - i] = obj5.Instance;
                    }
                }
                else
                {
                    functionObject.Params[(paramCount - 1) - i] = obj4;
                }
            }
            if (this.debugging)
            {
                this.callstack.Add(csr);
            }
            if (functionObject.Kind == MemberKind.Constructor)
            {
                if (functionObject.Owner.HasModifier(Modifier.Abstract))
                {
                    this.scripter.ToScriptObject(v).Instance = new object();
                }
                else
                {
                    this.scripter.ToScriptObject(v).Instance = functionObject.InvokeConstructor();
                }
            }
            else
            {
                object obj6;
                if (functionObject.Static)
                {
                    if (v == null)
                    {
                        obj6 = functionObject.InvokeMethod(null);
                    }
                    else
                    {
                        Type importedType = (v as ClassObject).ImportedType;
                        obj6 = functionObject.InvokeMethod(importedType);
                    }
                }
                else if (v is ObjectObject)
                {
                    obj6 = functionObject.InvokeMethod((v as ObjectObject).Instance);
                }
                else
                {
                    obj6 = functionObject.InvokeMethod(v);
                }
                if (this.r.res != 0)
                {
                    this.PutValue(this.r.res, obj6);
                }
            }
            if (this.debugging)
            {
                this.callstack.Pop();
            }
            this.Terminated = terminated;
            this.scripter.Owner.State = state;
            this.n++;
        }

        private void OperIncByte()
        {
            byte valueAsByte = this.GetValueAsByte(this.r.arg1);
            this.PutValue(this.r.res, valueAsByte = (byte) (valueAsByte + 1));
            this.n++;
        }

        private void OperIncChar()
        {
            char valueAsChar = this.GetValueAsChar(this.r.arg1);
            this.PutValue(this.r.res, valueAsChar = (char) (valueAsChar + '\x0001'));
            this.n++;
        }

        private void OperIncDecimal()
        {
            decimal valueAsDecimal = this.GetValueAsDecimal(this.r.arg1);
            this.PutValueAsDecimal(this.r.res, ++valueAsDecimal);//valueAsDecimal = decimal_op_Increment(valueAsDecimal));
            this.n++;
        }

        private decimal decimal_op_Increment(decimal valueAsDecimal) {
            return valueAsDecimal++;
        }

        private void OperIncDouble()
        {
            double valueAsDouble = this.GetValueAsDouble(this.r.arg1);
            this.PutValueAsDouble(this.r.res, ++valueAsDouble);
            this.n++;
        }

        private void OperIncFloat()
        {
            float valueAsFloat = this.GetValueAsFloat(this.r.arg1);
            this.PutValueAsFloat(this.r.res, ++valueAsFloat);
            this.n++;
        }

        private void OperIncInt()
        {
            int valueAsInt = this.GetValueAsInt(this.r.arg1);
            this.PutValueAsInt(this.r.res, ++valueAsInt);
            this.n++;
        }

        private void OperIncLong()
        {
            long valueAsLong = this.GetValueAsLong(this.r.arg1);
            this.PutValueAsLong(this.r.res, valueAsLong += 1L);
            this.n++;
        }

        private void OperIncSbyte()
        {
            sbyte num = (sbyte) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(sbyte));
            this.PutValue(this.r.res, num = (sbyte) (num + 1));
            this.n++;
        }

        private void OperIncShort()
        {
            short valueAsShort = this.GetValueAsShort(this.r.arg1);
            this.PutValue(this.r.res, valueAsShort = (short) (valueAsShort + 1));
            this.n++;
        }

        private void OperIncUint()
        {
            uint num = (uint) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(uint));
            this.PutValue(this.r.res, ++num);
            this.n++;
        }

        private void OperIncUlong()
        {
            ulong num = (ulong) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(ulong));
            this.PutValue(this.r.res, num += (ulong) 1L);
            this.n++;
        }

        private void OperIncUshort()
        {
            ushort num = (ushort) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg1)), typeof(ushort));
            this.PutValue(this.r.res, num = (ushort) (num + 1));
            this.n++;
        }

        private void OperInequalityBool()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsBool(this.r.arg1) != this.GetValueAsBool(this.r.arg2));
            this.n++;
        }

        private void OperInequalityDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) != this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperInequalityDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) != this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperInequalityFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) != this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperInequalityInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) != this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperInequalityLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) != this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperInequalityObject()
        {
            this.PutValueAsBool(this.r.res, this.GetValue(this.r.arg1) != this.GetValue(this.r.arg2));
            this.n++;
        }

        private void OperInequalityString()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsString(this.r.arg1) != this.GetValueAsString(this.r.arg2));
            this.n++;
        }

        private void OperInequalityUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) != this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperInequalityUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) != this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperInitMethod()
        {
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            this.n++;
            functionObject.Init = this[this.n + 1];
        }

        private void OperIs()
        {
            object v = this.GetValue(this.r.arg1);
            if (v == null)
            {
                this.PutValue(this.r.res, false);
            }
            else
            {
                ObjectObject obj3 = this.scripter.ToScriptObject(v);
                if (obj3.Class_Object.Id == this.r.arg2)
                {
                    this.PutValue(this.r.res, true);
                }
                else if (obj3.Class_Object.IsValueType)
                {
                    ClassObject obj4 = obj3.Class_Object;
                    ClassObject classObject = this.GetClassObject(this.r.arg2);
                    if (classObject.Class_Kind == ClassKind.Interface)
                    {
                        bool flag = obj4.Implements(classObject);
                        this.PutValue(this.r.res, flag);
                    }
                    else
                    {
                        bool flag2 = obj4.InheritsFrom(classObject);
                        this.PutValue(this.r.res, flag2);
                    }
                }
                else
                {
                    ClassObject obj6 = obj3.Class_Object;
                    ClassObject a = this.GetClassObject(this.r.arg2);
                    bool flag3 = obj6.InheritsFrom(a);
                    this.PutValue(this.r.res, flag3);
                }
            }
            this.n++;
        }

        private void OperLe()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) <= ((int) obj3));
            this.n++;
        }

        private void OperLeftShiftInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) << this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLeftShiftLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) << this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLeftShiftUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) << this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperLeftShiftUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) << this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperLessThanDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) < this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperLessThanDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) < this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperLessThanFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) < this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperLessThanInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) < this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLessThanLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) < this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualDecimal()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDecimal(this.r.arg1) <= this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualDouble()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsDouble(this.r.arg1) <= this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualFloat()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsFloat(this.r.arg1) <= this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualInt()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) <= this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualLong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) <= this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualString()
        {
            int num = string.Compare(this.GetValueAsString(this.r.arg1), this.GetValueAsString(this.r.arg2));
            this.PutValueAsBool(this.r.res, num <= 0);
            this.n++;
        }

        private void OperLessThanOrEqualUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) <= this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLessThanOrEqualUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) <= this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperLessThanString()
        {
            int num = string.Compare(this.GetValueAsString(this.r.arg1), this.GetValueAsString(this.r.arg2));
            this.PutValueAsBool(this.r.res, num < 0);
            this.n++;
        }

        private void OperLessThanUint()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsInt(this.r.arg1) < this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperLessThanUlong()
        {
            this.PutValueAsBool(this.r.res, this.GetValueAsLong(this.r.arg1) < this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperLock()
        {
            Monitor.Enter(this.GetObjectObject(this.r.arg1).Instance);
            this.n++;
        }

        private void OperLogicalNegationBool()
        {
            this.PutValue(this.r.res, !this.GetValueAsBool(this.r.arg1));
            this.n++;
        }

        private void OperLt()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) < ((int) obj3));
            this.n++;
        }

        private void OperMinus()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) - ((int) obj3));
            this.n++;
        }

        private void OperMult()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) * ((int) obj3));
            this.n++;
        }

        private void OperMultiplicationDecimal()
        {
            this.PutValueAsDecimal(this.r.res, this.GetValueAsDecimal(this.r.arg1) * this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperMultiplicationDouble()
        {
            this.PutValueAsDouble(this.r.res, this.GetValueAsDouble(this.r.arg1) * this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperMultiplicationFloat()
        {
            this.PutValueAsFloat(this.r.res, this.GetValueAsFloat(this.r.arg1) * this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperMultiplicationInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) * this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperMultiplicationLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) * this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperMultiplicationUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) * this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperMultiplicationUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) * this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperNe()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, obj2 != obj3);
            this.n++;
        }

        private void OperNeDelegates()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            if ((obj2 == null) || (obj3 == null))
            {
                this.PutVal(this.r.res, obj2 != obj3);
            }
            else
            {
                ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
                ObjectObject obj5 = this.GetObjectObject(this.r.arg2);
                this.PutVal(this.r.res, !this.CompareDelegates(objectObject, obj5));
            }
            this.n++;
        }

        private void OperNegationDecimal()
        {
            this.PutValueAsDecimal(this.r.res, -this.GetValueAsDecimal(this.r.arg1));
            this.n++;
        }

        private void OperNegationDouble()
        {
            this.PutValueAsDouble(this.r.res, -this.GetValueAsDouble(this.r.arg1));
            this.n++;
        }

        private void OperNegationFloat()
        {
            this.PutValueAsFloat(this.r.res, -this.GetValueAsFloat(this.r.arg1));
            this.n++;
        }

        private void OperNegationInt()
        {
            this.PutValue(this.r.res, -this.GetValueAsInt(this.r.arg1));
            this.n++;
        }

        private void OperNegationLong()
        {
            this.PutValueAsLong(this.r.res, -this.GetValueAsLong(this.r.arg1));
            this.n++;
        }

        private void OperNop()
        {
            this.n++;
        }

        private void OperOnError()
        {
            this.n++;
        }

        private void OperPlus()
        {
            object obj2 = this.GetValue(this.r.arg1);
            object obj3 = this.GetValue(this.r.arg2);
            this.PutValue(this.r.res, ((int) obj2) + ((int) obj3));
            this.n++;
        }

        private void OperPrint()
        {
            object obj2 = this.GetValue(this.r.arg1);
            if (obj2 == null)
            {
                Console.Write("null");
            }
            else
            {
                Console.Write(obj2.ToString());
            }
            this.n++;
        }

        private void OperPush()
        {
            this.stack.Push(this.GetValue(this.r.arg1));
            this.n++;
        }

        private void OperRemainderDecimal()
        {
            this.PutValueAsDecimal(this.r.res, this.GetValueAsDecimal(this.r.arg1) % this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperRemainderDouble()
        {
            this.PutValueAsDouble(this.r.res, this.GetValueAsDouble(this.r.arg1) % this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperRemainderFloat()
        {
            this.PutValueAsFloat(this.r.res, this.GetValueAsFloat(this.r.arg1) % this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperRemainderInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) % this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperRemainderLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) % this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperRemainderUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) % this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperRemainderUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) % this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperRestoreCheckedState()
        {
            this.RestoreCheckedState();
            this.n++;
        }

        private void OperResume()
        {
            this.goto_line = this.resume_stack.Peek();
            this.resume_stack.Pop();
            this.OperGotoContinue();
        }

        private void OperResumeNext()
        {
            this.goto_line = this.resume_stack.Peek() + 1;
            this.resume_stack.Pop();
            this.OperGotoContinue();
        }

        private void OperRet()
        {
            if (this.debugging)
            {
                this.callstack.Pop();
            }
            FunctionObject functionObject = this.GetFunctionObject(this.r.arg1);
            this.n = (int) this.stack.Pop();
            this.r = (ProgRec) this.prog[this.n];
            object obj3 = this.GetValue(functionObject.ResultId);
            functionObject.DeallocateSub();
            if (this.r.res != 0)
            {
                this.PutValue(this.r.res, obj3);
            }
            this.n++;
        }

        private void OperRightShiftInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) >> this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperRightShiftLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) >> this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperRightShiftUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) >> this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperRightShiftUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) >> this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperSetDefault()
        {
            int id = this.r.arg1;
            int num2 = this.r.arg2;
            this.GetPropertyObject(id).IsDefault = true;
            this.n++;
        }

        private void OperSetupDelegate()
        {
            ObjectObject obj2 = this.PopObjectObject();
            FunctionObject f = this.PopFunctionObject();
            object x = this.stack.Pop();
            obj2.AddInvocation(x, f);
            this.n++;
        }

        private void OperSetupIndexObject()
        {
            this.GetIndexObject(this.r.arg1).Setup();
            this.n++;
        }

        private void OperSubDelegates()
        {
            object obj6;
            FunctionObject obj7;
            bool flag;
            ObjectObject objectObject = this.GetObjectObject(this.r.arg1);
            ObjectObject obj3 = this.GetObjectObject(this.r.arg2);
            ObjectObject obj5 = objectObject.Class_Object.CreateObject();
            for (flag = objectObject.FindFirstInvocation(out obj6, out obj7); flag; flag = objectObject.FindNextInvocation(out obj6, out obj7))
            {
                obj5.AddInvocation(obj6, obj7);
            }
            for (flag = obj3.FindFirstInvocation(out obj6, out obj7); flag; flag = obj3.FindNextInvocation(out obj6, out obj7))
            {
                obj5.SubInvocation(obj6, obj7);
            }
            this.PutVal(this.r.res, obj5);
            this.n++;
        }

        private void OperSubtractionDecimal()
        {
            this.PutValueAsDecimal(this.r.res, this.GetValueAsDecimal(this.r.arg1) - this.GetValueAsDecimal(this.r.arg2));
            this.n++;
        }

        private void OperSubtractionDouble()
        {
            this.PutValueAsDouble(this.r.res, this.GetValueAsDouble(this.r.arg1) - this.GetValueAsDouble(this.r.arg2));
            this.n++;
        }

        private void OperSubtractionFloat()
        {
            this.PutValueAsFloat(this.r.res, this.GetValueAsFloat(this.r.arg1) - this.GetValueAsFloat(this.r.arg2));
            this.n++;
        }

        private void OperSubtractionInt()
        {
            this.PutValueAsInt(this.r.res, this.GetValueAsInt(this.r.arg1) - this.GetValueAsInt(this.r.arg2));
            this.n++;
        }

        private void OperSubtractionLong()
        {
            this.PutValueAsLong(this.r.res, this.GetValueAsLong(this.r.arg1) - this.GetValueAsLong(this.r.arg2));
            this.n++;
        }

        private void OperSubtractionUint()
        {
            this.PutValue(this.r.res, (uint) (this.GetValueAsInt(this.r.arg1) - this.GetValueAsInt(this.r.arg2)));
            this.n++;
        }

        private void OperSubtractionUlong()
        {
            this.PutValue(this.r.res, (ulong) (this.GetValueAsLong(this.r.arg1) - this.GetValueAsLong(this.r.arg2)));
            this.n++;
        }

        private void OperSwappedArguments()
        {
            this.scripter.SwappedArguments = (bool) this.scripter.GetValue(this.r.arg1);
            this.n++;
        }

        private void OperThrow()
        {
            if (this.r.arg1 <= 0)
            {
                throw this.scripter.LastError.E;
            }
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ObjectObject anObject = this.scripter.ToScriptObject(this.GetValue(this.r.arg1));
            if (!classObject.Imported)
            {
                this.custom_ex_list.AddObject(anObject.Instance as Exception, anObject);
            }
            throw (anObject.Instance as Exception);
        }

        private void OperToBoolean()
        {
            this.PutValue(this.r.res, ConvertHelper.ToBoolean(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToByte()
        {
            this.PutValue(this.r.res, ConvertHelper.ToByte(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToChar()
        {
            this.PutValue(this.r.res, ConvertHelper.ToChar(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToCharArray()
        {
            string str = ConvertHelper.ToString(this.GetValue(this.r.arg2));
            char[] chArray = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                chArray[i] = str[i];
            }
            this.PutValue(this.r.arg1, chArray);
            this.n++;
        }

        private void OperToDecimal()
        {
            this.PutValue(this.r.res, ConvertHelper.ToDecimal(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToDouble()
        {
            this.PutValue(this.r.res, ConvertHelper.ToDouble(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToEnum()
        {
            ClassObject classObject = this.GetClassObject(this.r.arg1);
            this.PutValue(this.r.res, ConvertHelper.ToEnum(classObject.RType, this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToFloat()
        {
            this.PutValue(this.r.res, ConvertHelper.ToFloat(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToInt()
        {
            this.PutValue(this.r.res, ConvertHelper.ToInt(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToLong()
        {
            this.PutValue(this.r.res, ConvertHelper.ToLong(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToSbyte()
        {
            int num = (int) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg2)), typeof(int));
            this.PutValue(this.r.res, (sbyte) num);
            this.n++;
        }

        private void OperToShort()
        {
            this.PutValue(this.r.res, ConvertHelper.ToShort(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToString()
        {
            this.PutValue(this.r.res, ConvertHelper.ToString(this.GetValue(this.r.arg2)));
            this.n++;
        }

        private void OperToUint()
        {
            long num = (long) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg2)), typeof(long));
            this.PutValue(this.r.res, (uint) num);
            this.n++;
        }

        private void OperToUlong()
        {
            object v = ConvertHelper.ToPrimitive(this.GetValue(this.r.arg2));
            this.PutValue(this.r.res, (ulong) ConvertHelper.ChangeType(v, typeof(ulong)));
            this.n++;
        }

        private void OperToUshort()
        {
            int num = (int) ConvertHelper.ChangeType(ConvertHelper.ToPrimitive(this.GetValue(this.r.arg2)), typeof(int));
            this.PutValue(this.r.res, (ushort) num);
            this.n++;
        }

        private void OperTryOff()
        {
            this.try_stack.Pop();
            this.n++;
        }

        private void OperTryOn()
        {
            this.try_stack.Push(this.n, this.r.arg1);
            this.n++;
        }

        private void OperTypeOf()
        {
            ClassObject classObject = this.GetClassObject(this.r.arg1);
            this.PutValue(this.r.res, this.scripter.ToScriptObject(classObject.RType));
            this.n++;
        }

        private void OperUnlock()
        {
            Monitor.Exit(this.GetObjectObject(this.r.arg1).Instance);
            this.n++;
        }

        public void Optimization()
        {
            for (int i = 1; i < this.card; i++)
            {
                int op = this[i].op;
                if (((op == this.OP_INC_INT) || (op == this.OP_INC_LONG)) || ((op == this.OP_DEC_INT) || (op == this.OP_DEC_LONG)))
                {
                    int num3 = i - 1;
                    while (this[num3].op == this.OP_NOP)
                    {
                        num3--;
                    }
                    if ((this[num3].op == this.OP_ASSIGN) && (this[i].arg1 == this[num3].arg2))
                    {
                        int res = this[num3].res;
                        bool flag = false;
                        for (int j = i + 1; j <= this.Card; j++)
                        {
                            if (((this[j].arg1 == res) || (this[j].arg2 == res)) || (this[j].res == res))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            this[num3].op = this.OP_NOP;
                        }
                    }
                }
                if ((this[i + 1].op == this.OP_ASSIGN) && (this[i].res == this[i + 1].arg2))
                {
                    bool flag2 = false;
                    for (int k = 0; k < this.detailed_operators.Count; k++)
                    {
                        if (this.detailed_operators.Items2[k] == op)
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        this[i].res = this[i + 1].res;
                        this[i + 1].op = this.OP_NOP;
                        this[i + 1].arg1 = 0;
                        this[i + 1].arg2 = 0;
                        this[i + 1].res = 0;
                    }
                }
            }
            this.RemoveNOP();
            this.symbol_table.SetupFastAccessRecords();
        }

        private bool PascalOrBasic(int j)
        {
            CSLite_Language language = this.GetLanguage(j);
            return ((language == CSLite_Language.VB) || (language == CSLite_Language.Pascal));
        }

        public FunctionObject PopFunctionObject()
        {
            return (FunctionObject) this.stack.Pop();
        }

        public ObjectObject PopObjectObject()
        {
            return this.scripter.ToScriptObject(this.stack.Pop());
        }

        private void ProcessAddAncestor()
        {
            int id = this.r.arg1;
            int num2 = this.r.arg2;
            ClassObject classObject = this.GetClassObject(id);
            ClassObject obj3 = this.GetClassObject(num2);
            if (obj3.Sealed && (classObject.Class_Kind != ClassKind.Subrange))
            {
                this.scripter.CreateErrorObjectEx("CS0509. '{0}' : cannot inherit from sealed class '{1}'.", new object[] { classObject.Name, obj3.Name });
            }
            else if (obj3.IsNamespace)
            {
                this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { obj3.Name, "namespace", "class" });
            }
            else if (classObject.AncestorIds.IndexOf(num2) >= 0)
            {
                this.scripter.CreateErrorObjectEx("CS0528. '{0}' is already listed in interface list.", new object[] { obj3.Name });
            }
            else if (obj3.InheritsFrom(classObject) || (obj3 == classObject))
            {
                this.scripter.CreateErrorObjectEx("CS0146. Circular base class definition between '{0}' and '{1}'.", new object[] { classObject.Name, obj3.Name });
            }
            else
            {
                if (!obj3.Imported && (MemberObject.CompareAccessibility(obj3, classObject) < 0))
                {
                    if (obj3.IsClass)
                    {
                        this.scripter.CreateErrorObjectEx("CS0060. Inconsistent accessibility: base class '{0}' is less accessible than class '{1}'.", new object[] { obj3.Name, classObject.Name });
                    }
                    else if (obj3.IsInterface)
                    {
                        this.scripter.CreateErrorObjectEx("CS0061. Inconsistent accessibility: base interface '{0}' is less accessible than interface '{1}.'", new object[] { obj3.Name, classObject.Name });
                    }
                }
                classObject.AncestorIds.Add(num2);
                if (obj3.Imported)
                {
                    Type baseType = obj3.ImportedType.BaseType;
                    while ((baseType != null) && (baseType != typeof(object)))
                    {
                        int avalue = this.scripter.symbol_table.RegisterType(baseType, false);
                        obj3.AncestorIds.Add(avalue);
                        baseType = baseType.BaseType;
                        obj3 = this.GetClassObject(avalue);
                    }
                }
            }
        }

        private void ProcessAddressOf()
        {
            int id = this.r.arg1;
            if (this.symbol_table[id].Kind == MemberKind.Ref)
            {
                int level = this.symbol_table[this.r.arg1].Level;
                int typeId = this.symbol_table[level].TypeId;
                ClassObject classObjectEx = this.GetClassObjectEx(typeId);
                int nameIndex = this.symbol_table[this.r.arg1].NameIndex;
                bool upcase = this.GetUpcase(this.n);
                MemberObject memberByNameIndex = classObjectEx.GetMemberByNameIndex(nameIndex, upcase);
                if (memberByNameIndex == null)
                {
                    string name = this.symbol_table[this.r.res].Name;
                    this.scripter.CreateErrorObjectEx("CS0103. The name '{0}' does not exist in the class or namespace '{1}'.", new object[] { name, classObjectEx.Name });
                    return;
                }
                id = memberByNameIndex.Id;
            }
            if (this.symbol_table[id].Kind != MemberKind.Method)
            {
                this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { this.symbol_table[id].FullName, this.symbol_table[id].Kind.ToString(), "method" });
            }
            else
            {
                FunctionObject functionObject = this.GetFunctionObject(id);
                int num5 = 0;
                for (int i = this.symbol_table.Card; i >= 1; i--)
                {
                    if (this.symbol_table[i].Kind == MemberKind.Type)
                    {
                        ClassObject classObject = this.GetClassObject(i);
                        if (classObject.IsDelegate)
                        {
                            FunctionObject patternMethod = classObject.PatternMethod;
                            if ((patternMethod != null) && FunctionObject.CompareHeaders(functionObject, patternMethod))
                            {
                                num5 = i;
                                break;
                            }
                        }
                    }
                }
                if (num5 == 0)
                {
                    bool flag2 = false;
                    for (int j = this.n; j <= this.card; j++)
                    {
                        if ((this[j].op == this.OP_PUSH) && (this[j].arg1 == this.r.res))
                        {
                            int num8 = this[j].res;
                            string str2 = this.symbol_table[num8].Name;
                            if (str2.Substring(0, 4) == "add_")
                            {
                                flag2 = true;
                                int num9 = this.GetFunctionObject(num8).Param_Ids[0];
                                num5 = this.symbol_table[num9].TypeId;
                                break;
                            }
                            if (str2.Substring(0, 7) == "remove_")
                            {
                                flag2 = true;
                                int num10 = this.GetFunctionObject(num8).Param_Ids[0];
                                num5 = this.symbol_table[num10].TypeId;
                                break;
                            }
                        }
                    }
                    if (!flag2)
                    {
                        this.scripter.CreateErrorObject("VB00005. Delegate type not found");
                        return;
                    }
                }
                this.r.op = this.OP_CREATE_OBJECT;
                this.r.arg1 = num5;
                this.r.arg2 = 0;
                int res = this.r.res;
                this.symbol_table[res].TypeId = num5;
                this.n++;
                this.InsertOperators(this.n, 4);
                this[this.n].op = this.OP_PUSH;
                if (functionObject.Static)
                {
                    this[this.n].arg1 = this.symbol_table[id].Level;
                }
                else
                {
                    this[this.n].arg1 = id;
                }
                this[this.n].arg2 = 0;
                this[this.n].res = num5;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = id;
                this[this.n].arg2 = 0;
                this[this.n].res = num5;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = res;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_SETUP_DELEGATE;
                this[this.n].arg1 = 0;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
            }
        }

        public void ProcessEvalOp(IntegerStack l)
        {
            bool upcase = this.GetUpcase();
            int nameIndex = this.symbol_table[this.r.res].NameIndex;
            int res = this.r.res;
            string name = this.symbol_table[res].Name;
            MemberObject memberByNameIndex = null;
            for (int i = l.Count - 1; i >= 0; i--)
            {
                MemberObject memberObject = this.GetMemberObject(l[i]);
                memberByNameIndex = memberObject.GetMemberByNameIndex(nameIndex, upcase);
                if ((memberByNameIndex != null) && ((memberByNameIndex.Kind != MemberKind.Constructor) || (i == (l.Count - 1))))
                {
                    int id = l.Peek();
                    MemberObject obj4 = this.GetMemberObject(id);
                    if (obj4.Static)
                    {
                        if (memberByNameIndex.Static)
                        {
                            this.r.op = this.OP_NOP;
                            this.ReplaceIdEx(this.r.res, memberByNameIndex.Id, this.n, true);
                            break;
                        }
                        if (this.GetCurrentClass(l).IsOuterMemberId(memberByNameIndex.Id))
                        {
                            this.scripter.CreateErrorObjectEx("CS0038. Cannot access a nonstatic member of outer type '{0}' via nested type '{1}'.", new object[] { memberObject.Name, this.GetCurrentClass(l).Name });
                            break;
                        }
                        memberByNameIndex = null;
                    }
                    else
                    {
                        if (memberByNameIndex.Static)
                        {
                            this.r.op = this.OP_NOP;
                            this.ReplaceIdEx(this.r.res, memberByNameIndex.Id, this.n, true);
                        }
                        else if (this.GetCurrentClass(l).IsOuterMemberId(memberByNameIndex.Id))
                        {
                            this.scripter.CreateErrorObjectEx("CS0038. Cannot access a nonstatic member of outer type '{0}' via nested type '{1}'.", new object[] { memberObject.Name, this.GetCurrentClass(l).Name });
                        }
                        else if (obj4.Kind == MemberKind.Type)
                        {
                            this.r.op = this.OP_NOP;
                        }
                        else
                        {
                            FunctionObject obj5 = (FunctionObject) obj4;
                            this.r.op = this.OP_CREATE_REFERENCE;
                            this.r.arg1 = obj5.ThisId;
                            this.symbol_table[this.r.res].Level = this.r.arg1;
                            this.symbol_table[this.r.res].Kind = MemberKind.Ref;
                            this.symbol_table[this.r.res].TypeId = this.symbol_table[memberByNameIndex.Id].TypeId;
                        }
                        break;
                    }
                }
            }
            if (memberByNameIndex == null)
            {
                memberByNameIndex = this.FindType(l, name, upcase);
                if (memberByNameIndex != null)
                {
                    this.r.op = this.OP_NOP;
                    this.ReplaceIdEx(this.r.res, memberByNameIndex.Id, this.n, true);
                }
                else if (this.symbol_table[res].Kind == MemberKind.Label)
                {
                    int num5 = this.EvalLabel(res);
                    if (num5 == 0)
                    {
                        this.scripter.CreateErrorObjectEx("Undeclared identifier '{0}'", new object[] { name });
                    }
                    else
                    {
                        this.r.op = this.OP_NOP;
                        this.ReplaceIdEx(this.r.res, num5, this.n, true);
                    }
                }
                else
                {
                    bool flag2 = false;
                    if (!this.GetExplicit(this.n))
                    {
                        int level = this.symbol_table[this.r.res].Level;
                        if ((this[this.n + 1].op == this.OP_ASSIGN) && (this[this.n + 1].arg1 == this.r.res))
                        {
                            this.r.op = this.OP_DECLARE_LOCAL_VARIABLE;
                            this.r.arg1 = this.r.res;
                            this.r.arg2 = level;
                            flag2 = true;
                        }
                        else
                        {
                            string str2 = this.symbol_table[this.r.res].Name;
                            for (int j = this.n; j >= 1; j--)
                            {
                                if (this[j].op == this.OP_DECLARE_LOCAL_VARIABLE)
                                {
                                    if (this[j].arg2 != level)
                                    {
                                        break;
                                    }
                                    int num8 = this[j].arg1;
                                    string str3 = this.symbol_table[num8].Name;
                                    if (CSLite_System.CompareStrings(str2, str3, upcase))
                                    {
                                        this.r.op = this.OP_NOP;
                                        this.ReplaceId(this.r.res, num8);
                                        flag2 = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (!flag2)
                    {
                        if (this.PascalOrBasic(this.n))
                        {
                            for (int k = 1; k <= this.card; k++)
                            {
                                if (this[k].op == this.OP_CREATE_CLASS)
                                {
                                    ClassObject classObject = this.GetClassObject(this[k].arg1);
                                    if (classObject.Static)
                                    {
                                        memberByNameIndex = classObject.GetMemberByNameIndex(nameIndex, upcase);
                                        if (memberByNameIndex != null)
                                        {
                                            this.r.op = this.OP_NOP;
                                            this.ReplaceId(this.r.res, memberByNameIndex.Id);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        if (this.scripter.DefaultInstanceMethods)
                        {
                            foreach (string str4 in this.scripter.UserInstances.Keys)
                            {
                                Type t = this.scripter.UserInstances[str4].GetType();
                                foreach (MethodInfo info in t.GetMethods())
                                {
                                    if (!CSLite_System.CompareStrings(name, info.Name, upcase))
                                    {
                                        continue;
                                    }
                                    int num10 = 0;
                                    for (int m = 1; m <= this.symbol_table.Card; m++)
                                    {
                                        if (((this.symbol_table[m].Level == 0) && (this.symbol_table[m].Kind == MemberKind.Var)) && (CSLite_System.CompareStrings(this.symbol_table[m].Name, str4, upcase) && (this.symbol_table[m].Val != null)))
                                        {
                                            num10 = m;
                                            break;
                                        }
                                    }
                                    if (num10 != 0)
                                    {
                                        int num12 = this.symbol_table.RegisterType(t, false);
                                        this.r.op = this.OP_CREATE_REFERENCE;
                                        this.r.arg1 = num10;
                                        this.symbol_table[this.r.res].Level = this.r.arg1;
                                        this.symbol_table[this.r.res].Kind = MemberKind.Ref;
                                        this.symbol_table[this.r.res].TypeId = num12;
                                        return;
                                    }
                                }
                                foreach (PropertyInfo info2 in t.GetProperties())
                                {
                                    if (!CSLite_System.CompareStrings(name, info2.Name, upcase))
                                    {
                                        continue;
                                    }
                                    int num13 = 0;
                                    for (int n = 1; n <= this.symbol_table.Card; n++)
                                    {
                                        if (((this.symbol_table[n].Level == 0) && (this.symbol_table[n].Kind == MemberKind.Var)) && (CSLite_System.CompareStrings(this.symbol_table[n].Name, str4, upcase) && (this.symbol_table[n].Val != null)))
                                        {
                                            num13 = n;
                                            break;
                                        }
                                    }
                                    if (num13 != 0)
                                    {
                                        int num15 = this.symbol_table.RegisterType(t, false);
                                        this.r.op = this.OP_CREATE_REFERENCE;
                                        this.r.arg1 = num13;
                                        this.symbol_table[this.r.res].Level = this.r.arg1;
                                        this.symbol_table[this.r.res].Kind = MemberKind.Ref;
                                        this.symbol_table[this.r.res].TypeId = num15;
                                        return;
                                    }
                                }
                            }
                        }
                        this.scripter.CreateErrorObjectEx("Undeclared identifier '{0}'", new object[] { name });
                    }
                }
            }
        }

        public void ProcessEvalType(IntegerStack l)
        {
            bool upcase = this.GetUpcase();
            int nameIndex = this.symbol_table[this.r.res].NameIndex;
            string name = this.symbol_table[this.r.res].Name;
            if (this.scripter.IsStandardType(this.r.res))
            {
                if (this.NextInstruction(this.n).op == this.OP_CREATE_TYPE_REFERENCE)
                {
                    name = name + "." + this.symbol_table[this.NextInstruction(this.n).res].Name;
                    this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { name });
                }
                else
                {
                    this.r.op = this.OP_NOP;
                }
                return;
            }
            MemberObject memberByNameIndex = null;
            for (int i = l.Count - 1; i >= 0; i--)
            {
                MemberObject memberObject = this.GetMemberObject(l[i]);
                if (memberObject.Kind == MemberKind.Type)
                {
                    if (memberObject.NameIndex == nameIndex)
                    {
                        memberByNameIndex = memberObject;
                        break;
                    }
                    memberByNameIndex = memberObject.GetMemberByNameIndex(nameIndex, upcase);
                    if (memberByNameIndex != null)
                    {
                        if (memberByNameIndex.Kind == MemberKind.Alias)
                        {
                            int id = memberByNameIndex.Id;
                            while (this.symbol_table[id].Kind == MemberKind.Alias)
                            {
                                memberByNameIndex = this.GetMemberObject(id);
                                id = this[memberByNameIndex.PCodeLine].res;
                            }
                            memberByNameIndex = this.GetMemberObject(id);
                            nameIndex = memberByNameIndex.NameIndex;
                        }
                        if ((memberByNameIndex.Kind == MemberKind.Type) && ((memberByNameIndex.NameIndex == nameIndex) || (upcase && (memberByNameIndex != null))))
                        {
                            break;
                        }
                    }
                }
            }
            if (memberByNameIndex == null)
            {
                int startIndex = CSLite_System.PosCh('[', name);
                if (startIndex > 0)
                {
                    if (this.GetClassObject(this.r.arg1).Imported)
                    {
                        memberByNameIndex = this.FindType(l, name, upcase);
                    }
                    if (memberByNameIndex == null)
                    {
                        string str2 = "Object" + name.Substring(startIndex);
                        memberByNameIndex = this.FindType(l, str2, upcase);
                        if (memberByNameIndex != null)
                        {
                            int num5 = this.symbol_table.AppVar();
                            this.symbol_table[num5].Name = name;
                            this.symbol_table[num5].Kind = MemberKind.Type;
                            int num6 = 0;
                            ClassObject obj5 = new ClassObject(this.scripter, num5, num6, ClassKind.Array);
                            obj5.Imported = true;
                            obj5.ImportedType = (memberByNameIndex as ClassObject).ImportedType;
                            obj5.RType = (memberByNameIndex as ClassObject).ImportedType;
                            this.PutVal(num5, obj5);
                            this.GetClassObject(num6).AddMember(obj5);
                            memberByNameIndex = obj5;
                        }
                    }
                }
            }
            if (memberByNameIndex == null)
            {
                memberByNameIndex = this.FindType(l, name, upcase);
            }
            if (memberByNameIndex == null)
            {
                this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { name });
                return;
            }
            this.r.op = this.OP_NOP;
        Label_02F4:
            this.n++;
            ProgRec rec = (ProgRec) this.prog[this.n];
            if (rec.op == this.OP_SEPARATOR)
            {
                goto Label_02F4;
            }
            if (rec.op == this.OP_CREATE_TYPE_REFERENCE)
            {
                this.r.op = this.OP_NOP;
                this.r = (ProgRec) this.prog[this.n];
                MemberObject obj7 = this.GetMemberObject(memberByNameIndex.Id);
                if (obj7.Kind != MemberKind.Type)
                {
                    this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { name });
                }
                else
                {
                    nameIndex = this.symbol_table[this.r.res].NameIndex;
                    memberByNameIndex = obj7.GetMemberByNameIndex(nameIndex, upcase);
                    if (memberByNameIndex != null)
                    {
                        goto Label_02F4;
                    }
                    name = this.symbol_table[this.r.res].FullName;
                    if (memberByNameIndex == null)
                    {
                        memberByNameIndex = this.FindType(l, name, upcase);
                    }
                    if (memberByNameIndex != null)
                    {
                        goto Label_02F4;
                    }
                    this.scripter.CreateErrorObjectEx("CS0246. The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?).", new object[] { name });
                }
            }
            if (memberByNameIndex != null)
            {
                this.scripter.CheckForbiddenType(memberByNameIndex.Id);
            }
            this.r.op = this.OP_NOP;
            if (!this.scripter.IsError())
            {
                this.ReplaceIdEx(this.r.res, memberByNameIndex.Id, this.n, true);
            }
        }

        public void PutVal(int id, object value)
        {
            this.symbol_table[id].Val = value;
        }

        public void PutValue(int id, object value)
        {
            this.symbol_table[id].Value = value;
        }

        public void PutValueAsBool(int id, bool value)
        {
            this.symbol_table[id].ValueAsBool = value;
        }

        public void PutValueAsDecimal(int id, decimal value)
        {
            this.symbol_table[id].ValueAsDecimal = value;
        }

        public void PutValueAsDouble(int id, double value)
        {
            this.symbol_table[id].ValueAsDouble = value;
        }

        public void PutValueAsFloat(int id, float value)
        {
            this.symbol_table[id].ValueAsFloat = value;
        }

        public void PutValueAsInt(int id, int value)
        {
            this.symbol_table[id].ValueAsInt = value;
        }

        public void PutValueAsLong(int id, long value)
        {
            this.symbol_table[id].ValueAsLong = value;
        }

        public void PutValueAsString(int id, string value)
        {
            this.symbol_table[id].ValueAsString = value;
        }

        private void RaiseError()
        {
            while (this.stack.Count > this.curr_stack_count)
            {
                this.stack.Pop();
            }
            if (this.try_stack.Count == 0)
            {
                if (this.PascalOrBasic(this.n))
                {
                    int n = this.n;
                    this.resume_stack.Push(this.n);
                    while (this[n].op != this.OP_INIT_METHOD)
                    {
                        n--;
                    }
                    do
                    {
                        n++;
                        if (this[n].op == this.OP_ONERROR)
                        {
                            this.n = n + 1;
                            return;
                        }
                    }
                    while (this[n].op != this.OP_END_METHOD);
                }
                this.Terminated = true;
                this.Paused = false;
                return;
            }
        Label_00C4:
            this.r = (ProgRec) this.prog[this.n];
            int op = this.r.op;
            while (true)
            {
                if (((op == this.OP_RET) || (op == this.OP_FINALLY)) || ((op == this.OP_CATCH) || (op == this.OP_HALT)))
                {
                    break;
                }
                if (op == this.OP_TRY_ON)
                {
                    this.OperTryOn();
                }
                else if (op == this.OP_TRY_OFF)
                {
                    this.OperTryOff();
                }
                else if (op == this.OP_HALT)
                {
                    this.OperHalt();
                }
                else
                {
                    this.n++;
                }
                this.r = (ProgRec) this.prog[this.n];
                op = this.r.op;
            }
            if (op == this.OP_RET)
            {
                this.OperRet();
                goto Label_00C4;
            }
            if (op == this.OP_CATCH)
            {
                if (this.try_stack.Legal(this.n))
                {
                    if (this.r.arg1 <= 0)
                    {
                        this.n++;
                        return;
                    }
                    Type t = this.scripter.LastError.E.GetType();
                    int id = this.symbol_table.RegisterType(t, false);
                    int typeId = this.symbol_table[this.r.arg1].TypeId;
                    if (typeId == id)
                    {
                        this.PutValue(this.r.arg1, this.scripter.LastError.E);
                        return;
                    }
                    ClassObject classObject = this.GetClassObject(id);
                    ClassObject a = this.GetClassObject(typeId);
                    if (classObject.InheritsFrom(a) || (a == classObject))
                    {
                        this.PutValue(this.r.arg1, this.scripter.LastError.E);
                        return;
                    }
                    int index = this.custom_ex_list.IndexOf(this.scripter.LastError.E);
                    if (index >= 0)
                    {
                        ObjectObject obj4 = this.custom_ex_list.Objects[index] as ObjectObject;
                        classObject = obj4.Class_Object;
                        if (classObject.InheritsFrom(a) || (a == classObject))
                        {
                            this.PutValue(this.r.arg1, obj4);
                            return;
                        }
                    }
                    this.n++;
                }
                else
                {
                    this.n++;
                }
                goto Label_00C4;
            }
            if ((op != this.OP_FINALLY) || this.try_stack.Legal(this.n))
            {
                return;
            }
            goto Label_00C4;
        }

        public IntegerStack RecreateClassStack(int init_n)
        {
             Module module = this.GetModule(init_n);
            if (module == null)
            {
                return null;
            }
            IntegerStack stack = new IntegerStack();
            for (int i = module.P1; i <= module.P2; i++)
            {
                if (this[i].op == this.OP_CREATE_CLASS)
                {
                    stack.Push(this[i].arg1);
                }
                else if (this[i].op == this.OP_END_CLASS)
                {
                    stack.Pop();
                }
                if (i == init_n)
                {
                    return stack;
                }
            }
            return stack;
        }

        public IntegerStack RecreateLevelStack(int init_n)
        {
             Module module = this.GetModule(init_n);
            if (module == null)
            {
                return null;
            }
            IntegerStack stack = new IntegerStack();
            for (int i = module.P1; i <= module.P2; i++)
            {
                if (this[i].op == this.OP_CREATE_METHOD)
                {
                    stack.Push(this[i].arg1);
                }
                else if (this[i].op == this.OP_END_METHOD)
                {
                    stack.Pop();
                }
                else if (this[i].op == this.OP_BEGIN_USING)
                {
                    stack.Push(this[i].arg1);
                }
                else if (this[i].op == this.OP_END_USING)
                {
                    stack.Pop();
                }
                if (i == init_n)
                {
                    return stack;
                }
            }
            return stack;
        }

        public void RemoveAllBreakpoints()
        {
            this.breakpoint_list.Clear();
        }

        public void RemoveBreakpoint(Breakpoint bp)
        {
            this.breakpoint_list.Remove(bp);
        }

        public void RemoveBreakpoint(string module_name, int line_number)
        {
            this.breakpoint_list.Remove(module_name, line_number);
        }

        public void RemoveEvalOp()
        {
            this.RemoveEvalOpEx(0, null);
        }

        public void RemoveEvalOpEx(int init_n, IntegerStack init_l)
        {
            if (!this.scripter.IsError())
            {
                IntegerStack stack;
                if (init_l == null)
                {
                    stack = new IntegerStack();
                }
                else
                {
                    stack = init_l.Clone();
                }
                for (int i = init_n + 1; i <= this.Card; i++)
                {
                    bool flag;
                    this.r = (ProgRec) this.prog[i];
                    int op = this.r.op;
                    this.n = i;
                    if (op == this.OP_CREATE_METHOD)
                    {
                        stack.Push(this.r.arg1);
                        int id = this.r.arg1;
                        int num4 = this.r.arg2;
                        MemberObject memberObject = this.GetMemberObject(id);
                        flag = memberObject.HasModifier(Modifier.New);
                        bool upcase = this.GetUpcase(this.n);
                        ClassObject classObject = this.GetClassObject(num4);
                        for (int j = 0; j < classObject.AncestorIds.Count; j++)
                        {
                            ClassObject obj4 = this.GetClassObject(classObject.AncestorIds[j]);
                            MemberObject memberByNameIndex = obj4.GetMemberByNameIndex(memberObject.NameIndex, upcase);
                            if (memberByNameIndex != null)
                            {
                                if ((!flag && !obj4.IsInterface) && (((!memberObject.HasModifier(Modifier.Override) || !memberByNameIndex.HasModifier(Modifier.Virtual)) && !memberObject.HasModifier(Modifier.Override)) && !memberObject.HasModifier(Modifier.Shadows)))
                                {
                                    this.scripter.CreateWarningObjectEx("CS0108. The keyword new is required on '{0}' because it hides inherited member '{1}'", new object[] { memberObject.FullName, obj4.FullName + "." + memberObject.Name });
                                }
                            }
                            else if (flag)
                            {
                                this.scripter.CreateWarningObjectEx("CS0109. The member '{0}' does not hide an inherited member. The new keyword is not required.", new object[] { memberObject.FullName });
                            }
                        }
                    }
                    else if (op == this.OP_RET)
                    {
                        stack.Pop();
                    }
                    else if (op == this.OP_BEGIN_USING)
                    {
                        int res = this.r.arg1;
                        while (this.symbol_table[res].Kind == MemberKind.Alias)
                        {
                            MemberObject obj6 = this.GetMemberObject(res);
                            res = this[obj6.PCodeLine].res;
                        }
                        string fullName = this.symbol_table[res].FullName;
                        if (this.scripter.CheckForbiddenNamespace(fullName))
                        {
                            this.scripter.CreateErrorObjectEx("CSLite0006. Use of forbidden namespace '{0}'.", new object[] { fullName });
                        }
                        stack.Push(res);
                    }
                    else if (op == this.OP_END_USING)
                    {
                        stack.Pop();
                    }
                    else if (op == this.OP_ADD_ANCESTOR)
                    {
                        this.ProcessAddAncestor();
                        if (this.scripter.IsError())
                        {
                            break;
                        }
                    }
                    else if (((op == this.OP_CREATE_FIELD) || (op == this.OP_CREATE_EVENT)) || (op == this.OP_CREATE_PROPERTY))
                    {
                        int num7 = this.r.arg1;
                        int num8 = this.r.arg2;
                        MemberObject obj7 = this.GetMemberObject(num7);
                        flag = obj7.HasModifier(Modifier.New);
                        ClassObject obj8 = this.GetClassObject(num8);
                        bool flag4 = this.GetUpcase(this.n);
                        for (int k = 0; k < obj8.AncestorIds.Count; k++)
                        {
                            ClassObject obj9 = this.GetClassObject(obj8.AncestorIds[k]);
                            if (obj9.GetMemberByNameIndex(obj7.NameIndex, flag4) != null)
                            {
                                if (!(((flag || obj9.IsInterface) || obj7.HasModifier(Modifier.Override)) || obj7.HasModifier(Modifier.Shadows)))
                                {
                                    this.scripter.CreateWarningObjectEx("CS0108. The keyword new is required on '{0}' because it hides inherited member '{1}'", new object[] { obj7.FullName, obj9.FullName + "." + obj7.Name });
                                }
                            }
                            else if (flag)
                            {
                                this.scripter.CreateWarningObjectEx("CS0109. The member '{0}' does not hide an inherited member. The new keyword is not required.", new object[] { obj7.FullName });
                            }
                        }
                    }
                    else if (op == this.OP_EVAL_TYPE)
                    {
                        this.ProcessEvalType(stack);
                        if (this.scripter.IsError())
                        {
                            break;
                        }
                    }
                    else if (op == this.OP_ADD_UNDERLYING_TYPE)
                    {
                        int num10 = this.r.arg1;
                        int num11 = this.r.arg2;
                        ClassObject obj10 = this.GetClassObject(num10);
                        ClassObject obj11 = this.GetClassObject(num11);
                        obj10.UnderlyingType = obj11;
                    }
                    else if (op == this.OP_EVAL)
                    {
                        if ((this[this.n + 1].op == this.OP_EVAL_TYPE) && (this[this.n + 1].res == this.r.res))
                        {
                            this.r.op = this.OP_NOP;
                        }
                        else
                        {
                            this.ProcessEvalOp(stack);
                            if (this.scripter.IsError())
                            {
                                break;
                            }
                        }
                    }
                }
                if (!this.scripter.IsError())
                {
                    for (int m = init_n + 1; m <= this.Card; m++)
                    {
                        this.r = (ProgRec) this.prog[m];
                        int num13 = this.r.op;
                        this.n = m;
                        if (num13 == this.OP_EVAL_BASE_TYPE)
                        {
                            ClassObject obj12 = this.GetClassObject(this.r.arg1);
                            ClassObject ancestorClass = obj12.AncestorClass;
                            if (ancestorClass == null)
                            {
                                int num14 = 0x10;
                                ancestorClass = this.GetClassObject(num14);
                                obj12.AncestorIds.Add(num14);
                            }
                            this.ReplaceId(this.r.res, ancestorClass.Id);
                            if (this.r.arg2 != 0)
                            {
                                FunctionObject obj14;
                                int num15 = ancestorClass.FindConstructorId(null, null, out obj14);
                                if (num15 != 0)
                                {
                                    this.ReplaceId(this.r.arg2, num15);
                                }
                            }
                            this.r.op = this.OP_NOP;
                        }
                        else if (num13 == this.OP_CAST)
                        {
                            this.symbol_table[this.r.res].TypeId = this.r.arg1;
                        }
                        else if (num13 == this.OP_ASSIGN_NAME)
                        {
                            this.symbol_table[this.r.res].NameIndex = this.symbol_table[this.r.arg2].NameIndex;
                        }
                    }
                }
            }
        }

        public void RemoveNOP()
        {
            for (int i = this.card; i > 0; i--)
            {
                if (this[i].op == this.OP_NOP)
                {
                    this.prog.RemoveAt(i);
                    this.card--;
                }
            }
            for (int j = 1; j <= this.card; j++)
            {
                this[j].FinalNumber = j;
            }
        }

        public void ReplaceId(int old_id, int new_id)
        {
            if (old_id != new_id)
            {
                for (int i = 1; i <= this.Card; i++)
                {
                    ProgRec rec = (ProgRec) this.prog[i];
                    if (rec.op != this.OP_SEPARATOR)
                    {
                        if (rec.arg1 == old_id)
                        {
                            rec.arg1 = new_id;
                        }
                        if (rec.arg2 == old_id)
                        {
                            rec.arg2 = new_id;
                        }
                        if (rec.res == old_id)
                        {
                            rec.res = new_id;
                        }
                    }
                }
                for (int j = old_id; j <= this.symbol_table.Card; j++)
                {
                    if ((this.symbol_table[j].Kind == MemberKind.Ref) && (this.symbol_table[j].Level == old_id))
                    {
                        this.symbol_table[j].Level = new_id;
                    }
                }
            }
        }

        public void ReplaceIdEx(int old_id, int new_id, int start_pos, bool local)
        {
            if (old_id != new_id)
            {
                MemberKind kind = this.symbol_table[new_id].Kind;
                for (int i = start_pos; i <= this.Card; i++)
                {
                    ProgRec rec = (ProgRec) this.prog[i];
                    int op = rec.op;
                    if (((op == this.OP_END_METHOD) || (op == this.OP_END_MODULE)) && (kind != MemberKind.Type))
                    {
                        break;
                    }
                    if (op != this.OP_SEPARATOR)
                    {
                        if (rec.arg1 == old_id)
                        {
                            rec.arg1 = new_id;
                        }
                        if (rec.arg2 == old_id)
                        {
                            rec.arg2 = new_id;
                        }
                        if (rec.res == old_id)
                        {
                            rec.res = new_id;
                        }
                    }
                }
                for (int j = old_id; j <= this.symbol_table.Card; j++)
                {
                    if ((this.symbol_table[j].Kind == MemberKind.Ref) && (this.symbol_table[j].Level == old_id))
                    {
                        this.symbol_table[j].Level = new_id;
                    }
                }
            }
        }

        public void Reset()
        {
            while (this.Card > 0)
            {
                this[this.Card].Reset();
                this.Card--;
            }
            this.card = 0;
            this.ResetRunStageStructs();
            this.breakpoint_list.Clear();
            this.get_item_list.Clear();
        }

        public void ResetRunStageStructs()
        {
            this.n = 0;
            this.stack.Clear();
            this.state_stack.Clear();
            this.try_stack.Clear();
            this.checked_stack.Clear();
            this.custom_ex_list.Clear();
            this.callstack.Clear();
            this.resume_stack.Clear();
            for (int i = 1; i <= this.card; i++)
            {
                if (this[i].op == this.OP_INIT_STATIC_VAR)
                {
                    this[i].res = 0;
                }
            }
        }

        private void RestoreCheckedState()
        {
            this.Checked = (bool) this.checked_stack.Pop();
        }

        public void RestoreState()
        {
            this.Card = this.state_stack.Pop();
            this.n = this.state_stack.Pop();
            this.RestoreCheckedState();
        }

        public void Run(RunMode run_mode)
        {
            this.Terminated = false;
            this.Paused = false;
            this.n++;
            if (!this.debugging || ((run_mode == RunMode.Run) && (this.breakpoint_list.Count == 0)))
            {
            Label_0363:
                if (!this.scripter.TerminatedFlag)
                {
                    if (this.scripter.Owner.rh != null)
                    {
                        this.scripter.Owner.rh(this.scripter.Owner);
                    }
                    try
                    {
                        this.r = (ProgRec) this.prog[this.n];
                        Oper oper2 = (Oper) this.arrProc[-this.r.op];
                        if (this.Checked)
                        {
                            oper2();
                        }
                        else
                        {
                            oper2();
                        }
                    }
                    catch (Exception innerException)
                    {
                        this.scripter.Error_List.Add(new ScriptError(this.scripter, innerException.Message));
                        this.scripter.LastError.E = innerException;
                        if (innerException.InnerException != null)
                        {
                            innerException = innerException.InnerException;
                            if (innerException != null)
                            {
                                this.scripter.Error_List.Add(new ScriptError(this.scripter, innerException.Message));
                                this.scripter.LastError.E = innerException;
                            }
                        }
                        this.RaiseError();
                    }
                    if (!this.Terminated)
                    {
                        goto Label_0363;
                    }
                }
                return;
            }
            int num = this.NextLine();
            int count = this.stack.Count;
            int currSubId = this.callstack.CurrSubId;
            this.breakpoint_list.Activate();
        Label_006C:
            if (this.scripter.TerminatedFlag)
            {
                return;
            }
        Label_0083:
            this.r = (ProgRec) this.prog[this.n];
            if (this.r.op != this.OP_SEPARATOR)
            {
                if (this.scripter.Owner.rh != null)
                {
                    this.scripter.Owner.rh(this.scripter.Owner);
                }
                try
                {
                    this.r = (ProgRec) this.prog[this.n];
                    Oper oper = (Oper) this.arrProc[-this.r.op];
                    if (this.Checked)
                    {
                        oper();
                    }
                    else
                    {
                        oper();
                    }
                }
                catch (Exception exception)
                {
                    this.scripter.Error_List.Add(new ScriptError(this.scripter, exception.Message));
                    this.scripter.LastError.E = exception;
                    if (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                        if (exception != null)
                        {
                            this.scripter.Error_List.Add(new ScriptError(this.scripter, exception.Message));
                            this.scripter.LastError.E = exception;
                        }
                    }
                    this.RaiseError();
                }
                if (this.Terminated)
                {
                    return;
                }
                goto Label_006C;
            }
            if (this.HasBreakpoint(this.n))
            {
                this.Paused = true;
            }
            else
            {
                switch (run_mode)
                {
                    case RunMode.Run:
                        this.n++;
                        goto Label_0083;

                    case RunMode.TraceInto:
                        while (this[this.n + 1].op == this.OP_SEPARATOR)
                        {
                            this.n++;
                        }
                        this.Paused = true;
                        break;

                    case RunMode.StepOver:
                        if (this.stack.Count > count)
                        {
                            this.n++;
                            goto Label_0083;
                        }
                        while (this[this.n + 1].op == this.OP_SEPARATOR)
                        {
                            this.n++;
                        }
                        this.Paused = true;
                        break;

                    case RunMode.NextLine:
                        if (this.n != num)
                        {
                            this.n++;
                            goto Label_0083;
                        }
                        while (this[this.n + 1].op == this.OP_SEPARATOR)
                        {
                            this.n++;
                        }
                        this.Paused = true;
                        break;

                    case RunMode.UntilReturn:
                        if (this.callstack.HasSubId(currSubId))
                        {
                            this.n++;
                            goto Label_0083;
                        }
                        while (this[this.n + 1].op == this.OP_SEPARATOR)
                        {
                            this.n++;
                        }
                        this.Paused = true;
                        break;

                    default:
                        goto Label_0083;
                }
            }
        }

        private void SaveCheckedState()
        {
            this.checked_stack.Push(this.Checked);
        }

        public void SaveState()
        {
            this.state_stack.Push(this.n);
            this.state_stack.Push(this.Card);
            this.SaveCheckedState();
        }

        public void SaveToStream(BinaryWriter bw,  Module m)
        {
            int num = m.P1;
            int num2 = m.P2;
            for (int i = num; i <= num2; i++)
            {
                this[i].SaveToStream(bw);
            }
        }

        public void SetInstruction(int line, int op, int arg1, int arg2, int res)
        {
            ProgRec rec = (ProgRec) this.prog[line];
            rec.op = op;
            rec.arg1 = arg1;
            rec.arg2 = arg2;
            rec.res = res;
            if ((op == this.OP_CREATE_REFERENCE) || (op == this.OP_CREATE_TYPE_REFERENCE))
            {
                this.symbol_table[res].Level = arg1;
                this.symbol_table[res].Kind = MemberKind.Ref;
            }
            else if (op == this.OP_CREATE_INDEX_OBJECT)
            {
                this.symbol_table[res].Level = arg1;
                this.symbol_table[res].Kind = MemberKind.Index;
            }
        }

        public void SetLabelHere(int label_id)
        {
            this.symbol_table.SetLabel(label_id, this.Card + 1);
        }

        public void SetTypeId(int id, int type_id)
        {
            this.symbol_table[id].TypeId = type_id;
        }

        public void SetTypes()
        {
            this.SetTypesEx(0);
        }

        public void SetTypesEx(int init_n)
        {
            if (!this.scripter.IsError())
            {
                FunctionObject functionObject = null;
                for (int i = init_n + 1; i <= this.Card; i++)
                {
                    this.r = (ProgRec) this.prog[i];
                    int op = this.r.op;
                    this.n = i;
                    if (op == this.OP_ASSIGN_TYPE)
                    {
                        this.scripter.Dump();
                        this.symbol_table[this.r.arg1].TypeId = this.r.arg2;
                        ClassObject obj3 = this.GetClassObject(this.r.arg2);
                        if (obj3.Class_Kind == ClassKind.Namespace)
                        {
                            this.scripter.CreateErrorObjectEx("CS0118. '{0}' denotes a '{1}' where a '{2}' was expected.", new object[] { obj3.Name, "namespace", "class" });
                            break;
                        }
                        MemberKind kind = this.symbol_table[this.r.arg1].Kind;
                        switch (kind)
                        {
                            case MemberKind.Method:
                                functionObject = this.GetFunctionObject(this.r.arg1);
                                if ((MemberObject.CompareAccessibility(obj3, functionObject) < 0) && (obj3.Id != functionObject.OwnerId))
                                {
                                    if (functionObject.Name == "op_Implicit")
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0056. Inconsistent accessibility: return type '{0}' is less accessible than operator '{1}'.", new object[] { obj3.Name, "Implicit" });
                                    }
                                    else if (functionObject.Name == "op_Explicit")
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0056. Inconsistent accessibility: return type '{0}' is less accessible than operator '{1}'.", new object[] { obj3.Name, "Explicit" });
                                    }
                                    else if (CSLite_System.PosCh('$', functionObject.Name) >= 0)
                                    {
                                        if (functionObject.Owner.IsDelegate)
                                        {
                                            this.scripter.CreateErrorObjectEx("CS0058. Inconsistent accessibility: parameter type '{0}' is less accessible than delegate '{1}'.", new object[] { obj3.Name, functionObject.Owner.Name });
                                        }
                                    }
                                    else
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0050. Inconsistent accessibility: return type '{0}' is less accessible than method '{1}'.", new object[] { obj3.Name, functionObject.Name });
                                    }
                                }
                                break;

                            case MemberKind.Field:
                            {
                                FieldObject fieldObject = this.GetFieldObject(this.r.arg1);
                                if ((MemberObject.CompareAccessibility(obj3, fieldObject) < 0) && (obj3.Id != fieldObject.OwnerId))
                                {
                                    this.scripter.CreateErrorObjectEx("CS0052. Inconsistent accessibility: field type '{0}' is less accessible than field '{1}'.", new object[] { obj3.Name, fieldObject.Name });
                                }
                                break;
                            }
                            case MemberKind.Event:
                            {
                                EventObject eventObject = this.GetEventObject(this.r.arg1);
                                if ((MemberObject.CompareAccessibility(obj3, eventObject) < 0) && (obj3.Id != eventObject.OwnerId))
                                {
                                    this.scripter.CreateErrorObjectEx("CS0053. Inconsistent accessibility: property type '{0}' is less accessible than property '{1}'.", new object[] { obj3.Name, eventObject.Name });
                                }
                                if (!obj3.IsDelegate)
                                {
                                    this.scripter.CreateErrorObjectEx("CS0066. '{0}': event must be of a delegate type.", new object[] { eventObject.Name });
                                }
                                break;
                            }
                            case MemberKind.Property:
                            {
                                PropertyObject propertyObject = this.GetPropertyObject(this.r.arg1);
                                if ((MemberObject.CompareAccessibility(obj3, propertyObject) < 0) && (obj3.Id != propertyObject.OwnerId))
                                {
                                    if (propertyObject.ParamCount == 0)
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0053. Inconsistent accessibility: property type '{0}' is less accessible than property '{1}'.", new object[] { obj3.Name, propertyObject.Name });
                                    }
                                    else
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0054. Inconsistent accessibility: indexer return type '{0}' is less accessible than indexer '{1}'.", new object[] { obj3.Name, propertyObject.Name });
                                    }
                                }
                                break;
                            }
                            default:
                                if ((((kind == MemberKind.Var) && (functionObject != null)) && (functionObject.HasParameter(this.r.arg1) && (MemberObject.CompareAccessibility(obj3, functionObject) < 0))) && (obj3.Id != functionObject.OwnerId))
                                {
                                    if ((functionObject.Name == "get_Item") || (functionObject.Name == "set_Item"))
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0055. Inconsistent accessibility: parameter type '{0}' is less accessible than indexer '{1}'.", new object[] { obj3.Name, functionObject.Name });
                                    }
                                    else if (functionObject.Name == "op_Implicit")
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0057. Inconsistent accessibility: parameter type '{0}' is less accessible than operator '{1}'.", new object[] { obj3.Name, "Implicit" });
                                    }
                                    else if (functionObject.Name == "op_Explicit")
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0057. Inconsistent accessibility: parameter type '{0}' is less accessible than operator '{1}'.", new object[] { obj3.Name, "Explicit" });
                                    }
                                    else if ((CSLite_System.PosCh('$', functionObject.Name) >= 0) && functionObject.Owner.IsDelegate)
                                    {
                                        this.scripter.CreateErrorObjectEx("CS0059. Inconsistent accessibility: parameter type '{0}' is less accessible than delegate '{1}'.", new object[] { obj3.Name, functionObject.Owner.Name });
                                    }
                                }
                                break;
                        }
                        this.r.op = this.OP_NOP;
                        continue;
                    }
                    if (op == this.OP_TYPEOF)
                    {
                        this.symbol_table[this.r.res].TypeId = this.symbol_table.TYPE_CLASS_id;
                        continue;
                    }
                    if (op == this.OP_CREATE_REF_TYPE)
                    {
                        int id = this.r.arg1;
                        int level = this.symbol_table[id].Level;
                        string str = this.symbol_table[id].Name + "&";
                        int num5 = 0;
                        for (int j = 1; j <= this.symbol_table.Card; j++)
                        {
                            if ((this.symbol_table[j].Level == level) && (this.symbol_table[j].Name == str))
                            {
                                num5 = j;
                                break;
                            }
                        }
                        if (num5 > 0)
                        {
                            this.ReplaceId(this.r.res, num5);
                        }
                        else
                        {
                            this.PutVal(this.r.res, this.GetVal(id));
                            this.symbol_table[this.r.arg1].Kind = MemberKind.Type;
                        }
                        continue;
                    }
                    if (op == this.OP_SET_REF_TYPE)
                    {
                        int typeId = this.symbol_table[this.r.arg1].TypeId;
                        int num8 = this.symbol_table[typeId].Level;
                        string str2 = this.symbol_table[typeId].Name + "&";
                        int num9 = 0;
                        for (int k = 1; k <= this.symbol_table.Card; k++)
                        {
                            if ((this.symbol_table[k].Level == num8) && (this.symbol_table[k].Name == str2))
                            {
                                num9 = k;
                                break;
                            }
                        }
                        if (num9 == 0)
                        {
                            num9 = this.symbol_table.AppVar();
                            this.PutVal(num9, this.GetVal(typeId));
                            this.symbol_table[num9].Kind = MemberKind.Type;
                            this.symbol_table[num9].Level = num8;
                            this.symbol_table[num9].Name = str2;
                        }
                        this.symbol_table[this.r.arg1].TypeId = num9;
                        for (int m = this.n; m <= this.Card; m++)
                        {
                            if ((this[m].op == this.OP_SET_REF_TYPE) && (this[m].arg1 == this.r.arg1))
                            {
                                this[m].op = this.OP_NOP;
                            }
                        }
                        continue;
                    }
                    if (op == this.OP_CREATE_OBJECT)
                    {
                        this.symbol_table[this.r.res].TypeId = this.r.arg1;
                        continue;
                    }
                    if (op == this.OP_AS)
                    {
                        this.symbol_table[this.r.res].TypeId = this.r.arg2;
                        continue;
                    }
                    if ((op == this.OP_ASSIGN) && (this[this.n - 1].op == this.OP_DECLARE_LOCAL_VARIABLE))
                    {
                        if ((this[this.n - 1].arg1 == this.r.res) && !this.GetExplicit(this.n))
                        {
                            this.symbol_table[this.r.res].TypeId = this.symbol_table[this.r.arg2].TypeId;
                        }
                        continue;
                    }
                    if ((op == this.OP_ASSIGN) && (this.symbol_table[this.r.arg1].TypeId == 0x10))
                    {
                        if (!this.GetStrict(this.n))
                        {
                            this.symbol_table[this.r.res].TypeId = this.symbol_table[this.r.arg2].TypeId;
                        }
                        continue;
                    }
                    if (op == this.OP_ADD_IMPLEMENTS)
                    {
                        int num12 = this.r.arg1;
                        this.GetMemberObject(num12).ImplementsId = this.r.res;
                        continue;
                    }
                    if (op == this.OP_BEGIN_CALL)
                    {
                        if (this.symbol_table[this.r.arg1].Name.IndexOf('[') >= 0)
                        {
                            this.r.op = this.OP_NOP;
                        }
                        continue;
                    }
                    if (op == this.OP_ADD_ARRAY_RANGE)
                    {
                        this.GetClassObject(this.r.arg1).RangeTypeId = this.r.arg2;
                        continue;
                    }
                    if (op == this.OP_ADD_ARRAY_INDEX)
                    {
                        ClassObject obj9 = this.GetClassObject(this.r.arg1);
                        obj9.IndexTypeId = this.r.arg2;
                        string str4 = this.symbol_table[this.r.arg2].Name;
                        int typeByName = this.symbol_table.LookupTypeByName(str4, true);
                        if (typeByName == 0)
                        {
                            typeByName = this.symbol_table.OBJECT_CLASS_id;
                        }
                        ClassObject obj10 = this.GetClassObject(typeByName);
                        if (obj10.ImportedType == null)
                        {
                            obj9.ImportedType = typeof(object);
                        }
                        else
                        {
                            obj9.ImportedType = obj10.ImportedType;
                        }
                        continue;
                    }
                    if (op == this.OP_CALL)
                    {
                        string elementTypeName = this.symbol_table[this.r.arg1].Name;
                        if (elementTypeName.IndexOf('[') < 0)
                        {
                            continue;
                        }
                        this.r.op = this.OP_CREATE_ARRAY_INSTANCE;
                        elementTypeName = CSLite_System.GetElementTypeName(elementTypeName);
                        bool flag = this.GetUpcase(this.n);
                        int num15 = this.symbol_table.LookupTypeByName(elementTypeName, flag);
                        if (num15 > 0)
                        {
                            this.r.arg1 = num15;
                            continue;
                        }
                        this.scripter.CreateErrorObjectEx("Undeclared identifier '{0}'", new object[] { elementTypeName });
                        break;
                    }
                    if ((op != this.OP_CREATE_REFERENCE) || (this.symbol_table[this.r.arg1].Kind != MemberKind.Type))
                    {
                        continue;
                    }
                    ClassObject classObject = this.GetClassObject(this.r.arg1);
                    if (this.scripter.ForbiddenTypes.IndexOf(classObject.FullName) >= 0)
                    {
                        this.scripter.CreateErrorObjectEx("CSLite0007. Use of forbidden type '{0}'.", new object[] { classObject.FullName });
                    }
                    int nameIndex = this.symbol_table[this.r.res].NameIndex;
                    string name = this.symbol_table[this.r.res].Name;
                    bool upcase = this.GetUpcase(this.n);
                    MemberObject memberByNameIndex = classObject.GetMemberByNameIndex(nameIndex, upcase);
                    if (((memberByNameIndex == null) && classObject.IsNamespace) && classObject.Imported)
                    {
                        memberByNameIndex = this.scripter.FindImportedNamespaceMember(classObject, name, upcase);
                    }
                    if (memberByNameIndex == null)
                    {
                        if ((this.GetLanguage(this.n) == CSLite_Language.Pascal) && CSLite_System.StrEql(name, "Create"))
                        {
                            int num17 = this[this.n].arg1;
                            int res = this[this.n].res;
                            int num19 = this.AppVar(this.GetCurrMethodId(), num17);
                            if ((this[this.n + 1].op == this.OP_BEGIN_CALL) && (this[this.n + 1].arg1 == this[this.n].res))
                            {
                                this[this.n].op = this.OP_CREATE_OBJECT;
                                this[this.n].res = num19;
                                this[this.n + 1].arg1 = num17;
                                int num20 = this.n + 1;
                                while (true)
                                {
                                    if ((this[num20].op == this.OP_PUSH) && (this[num20].res == res))
                                    {
                                        this[num20].res = num17;
                                    }
                                    if ((this[num20].op == this.OP_CALL) && (this[num20].arg1 == res))
                                    {
                                        this[num20].arg1 = num17;
                                        break;
                                    }
                                    num20++;
                                }
                                this[num20 - 1].arg1 = num19;
                                this.ReplaceId(this[num20].res, num19);
                                this[num20].res = 0;
                                continue;
                            }
                            this[this.n].op = this.OP_CREATE_OBJECT;
                            this[this.n].res = num19;
                            this.n++;
                            this.InsertOperators(this.n, 3);
                            this[this.n].op = this.OP_BEGIN_CALL;
                            this[this.n].arg1 = num17;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_PUSH;
                            this[this.n].arg1 = num19;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_CALL;
                            this[this.n].arg1 = num17;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.ReplaceId(res, num19);
                            this.n -= 3;
                            continue;
                        }
                        this.scripter.CreateErrorObjectEx("CS0103. The name '{0}' does not exist in the class or namespace '{1}'.", new object[] { name, classObject.Name });
                        break;
                    }
                    if (!memberByNameIndex.Static)
                    {
                        if (this[this.n + 1].op == this.OP_ADDRESS_OF)
                        {
                            goto Label_136C;
                        }
                        memberByNameIndex = classObject.GetStaticMemberByNameIndex(nameIndex, upcase);
                        if (memberByNameIndex != null)
                        {
                            goto Label_136C;
                        }
                        if ((this.GetLanguage(this.n) == CSLite_Language.Pascal) && CSLite_System.StrEql(name, "Create"))
                        {
                            int num21 = this[this.n].arg1;
                            int num22 = this[this.n].res;
                            int num23 = this.AppVar(this.GetCurrMethodId(), num21);
                            if ((this[this.n + 1].op == this.OP_BEGIN_CALL) && (this[this.n + 1].arg1 == this[this.n].res))
                            {
                                this[this.n].op = this.OP_CREATE_OBJECT;
                                this[this.n].res = num23;
                                this[this.n + 1].arg1 = num21;
                                int num24 = this.n + 1;
                                while (true)
                                {
                                    if ((this[num24].op == this.OP_PUSH) && (this[num24].res == num22))
                                    {
                                        this[num24].res = num21;
                                    }
                                    if ((this[num24].op == this.OP_CALL) && (this[num24].arg1 == num22))
                                    {
                                        this[num24].arg1 = num21;
                                        break;
                                    }
                                    num24++;
                                }
                                this[num24 - 1].arg1 = num23;
                                this.ReplaceId(this[num24].res, num23);
                                this[num24].res = 0;
                                continue;
                            }
                            this[this.n].op = this.OP_CREATE_OBJECT;
                            this[this.n].res = num23;
                            this.n++;
                            this.InsertOperators(this.n, 3);
                            this[this.n].op = this.OP_BEGIN_CALL;
                            this[this.n].arg1 = num21;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_PUSH;
                            this[this.n].arg1 = num23;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.n++;
                            this[this.n].op = this.OP_CALL;
                            this[this.n].arg1 = num21;
                            this[this.n].arg2 = 0;
                            this[this.n].res = 0;
                            this.ReplaceId(num22, num23);
                            this.n -= 3;
                            continue;
                        }
                        this.scripter.CreateErrorObjectEx("CS0120. An object reference is required for the nonstatic field, method, or property '{0}'.", new object[] { name });
                        break;
                    }
                    int ownerId = memberByNameIndex.OwnerId;
                    string fullName = this.symbol_table[ownerId].FullName;
                    if (this.scripter.CheckForbiddenNamespace(fullName))
                    {
                        this.scripter.CreateErrorObjectEx("CSLite0006. Use of forbidden namespace '{0}'.", new object[] { fullName });
                    }
                Label_136C:
                    this.r.op = this.OP_NOP;
                    this.ReplaceIdEx(this.r.res, memberByNameIndex.Id, this.n, true);
                }
            }
        }

        public void SetUpcase(int line, Upcase value)
        {
            ProgRec rec = (ProgRec) this.prog[line];
            rec.upcase = value;
        }

        private bool SetupDetailedBinaryOperator(int op, string str_op, Contact_Integers details)
        {
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            int id = this.symbol_table[this.r.arg2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            classObject = this.GetClassObject(typeId);
            if (classObject.IsSubrange)
            {
                typeId = classObject.AncestorIds[0];
                classObject = this.GetClassObject(typeId);
            }
            ClassObject obj3 = this.GetClassObject(id);
            object obj4 = this.overloadable_binary_operators_str[op];
            if (obj4 != null)
            {
                string str = (string) obj4;
                if (this.TryOverloadableBinaryOperator(op, str, classObject, obj3))
                {
                    return true;
                }
            }
            if (this.scripter.conversion.ExistsImplicitNumericConstConversion(this.scripter, this.r.arg1, this.r.arg2) && this.TryDetailedBinaryOperator(details, obj3, obj3))
            {
                return true;
            }
            if (this.scripter.conversion.ExistsImplicitNumericConstConversion(this.scripter, this.r.arg2, this.r.arg1) && this.TryDetailedBinaryOperator(details, classObject, classObject))
            {
                return true;
            }
            if (this.TryDetailedBinaryOperator(details, classObject, obj3))
            {
                return true;
            }
            if (((typeId == 12) || (id == 12)) && (op == this.OP_PLUS))
            {
                this.r.op = this.OP_ADDITION_STRING;
                this.symbol_table[this.r.res].TypeId = 12;
                return true;
            }
            if ((op == this.OP_EQ) || (op == this.OP_NE))
            {
                if (typeId == id)
                {
                    return true;
                }
                if (typeId == 0x10)
                {
                    return true;
                }
                if (id == 0x10)
                {
                    return true;
                }
            }
            string name = this.symbol_table[typeId].Name;
            string str3 = this.symbol_table[id].Name;
            this.scripter.CreateErrorObjectEx("CS0019. Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'.", new object[] { str_op, name, str3 });
            return false;
        }

        private bool SetupDetailedUnaryOperator(int op, string str_op, Contact_Integers details)
        {
            int typeId = this.symbol_table[this.r.arg1].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            object obj3 = this.overloadable_unary_operators_str[op];
            if (obj3 != null)
            {
                string str = (string) obj3;
                if (this.TryOverloadableUnaryOperator(str, classObject))
                {
                    return true;
                }
            }
            if (this.TryDetailedUnaryOperator(details, classObject))
            {
                return true;
            }
            string name = this.symbol_table[typeId].Name;
            this.scripter.CreateErrorObjectEx("CS0023. Operator '{0}' cannot be applied to operand of type '{1}'.", new object[] { str_op, name });
            return false;
        }

        private bool TryBinaryOperator(int type_id, ClassObject c1, ClassObject c2)
        {
            ClassObject classObject = this.GetClassObject(type_id);
            if (this.scripter.MatchTypes(c1, classObject) && this.scripter.MatchTypes(c2, classObject))
            {
                if (c1.Class_Kind == ClassKind.Enum)
                {
                    this.symbol_table[this.r.res].TypeId = c1.Id;
                }
                else if (c2.Class_Kind == ClassKind.Enum)
                {
                    this.symbol_table[this.r.res].TypeId = c2.Id;
                }
                else
                {
                    this.symbol_table[this.r.res].TypeId = classObject.Id;
                }
                return true;
            }
            return false;
        }

        private bool TryDetailedBinaryOperator(Contact_Integers details, ClassObject c1, ClassObject c2)
        {
            for (int i = 0; i < details.Count; i++)
            {
                if (this.TryBinaryOperator(details.Items1[i], c1, c2))
                {
                    this.r.op = details.Items2[i];
                    return true;
                }
            }
            return false;
        }

        private bool TryDetailedUnaryOperator(Contact_Integers details, ClassObject c1)
        {
            for (int i = 0; i < details.Count; i++)
            {
                if (details.Items1[i] == c1.Id)
                {
                    this.r.op = details.Items2[i];
                    this.symbol_table[this.r.res].TypeId = c1.Id;
                    return true;
                }
            }
            for (int j = 0; j < details.Count; j++)
            {
                if (this.TryUnaryOperator(details.Items1[j], c1))
                {
                    this.r.op = details.Items2[j];
                    return true;
                }
            }
            return false;
        }

        private bool TryOverloadableBinaryOperator(int op, string operator_name, ClassObject c1, ClassObject c2)
        {
            int num;
            foreach (string str in this.scripter.OperatorHelpers.Keys)
            {
                ClassObject classObject = null;
                FunctionObject best = null;
                int num2 = 0;
                IntegerList list = new IntegerList(false);
                IntegerList a = new IntegerList(true);
                a.Add(this.r.arg1);
                a.Add(this.r.arg2);
                a.Add(this.symbol_table.FALSE_id);
                IntegerList list3 = new IntegerList(true);
                list3.Add(0);
                list3.Add(0);
                list3.Add(0);
                if (operator_name == str)
                {
                    MethodInfo info = (MethodInfo) this.scripter.OperatorHelpers[str];
                    Type reflectedType = info.ReflectedType;
                    int num3 = this.symbol_table.RegisterType(reflectedType, false);
                    int id = this.symbol_table.RegisterMethod(info, num3);
                    classObject = this.scripter.GetClassObject(num3);
                    FunctionObject functionObject = this.scripter.GetFunctionObject(id);
                    classObject.AddApplicableMethod(functionObject, a, list3, num2, ref best, ref list);
                }
                num = 0;
                if (classObject != null)
                {
                    classObject.CompressApplicableMethodList(a, list);
                    if (list.Count >= 1)
                    {
                        num = list[0];
                    }
                }
                if (num != 0)
                {
                    this.r.op = this.OP_NOP;
                    int res = this.r.res;
                    this.n++;
                    this.InsertOperators(this.n, 5);
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = a[0];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = a[1];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = a[2];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = classObject.Id;
                    this[this.n].arg2 = 0;
                    this[this.n].res = 0;
                    this.n++;
                    this[this.n].op = this.OP_CALL_SIMPLE;
                    this[this.n].arg1 = num;
                    this[this.n].arg2 = 2;
                    this[this.n].res = res;
                    this.symbol_table[this[this.n].res].TypeId = this.symbol_table[num].TypeId;
                    return true;
                }
            }
            foreach (string str2 in this.scripter.OperatorHelpers.Keys)
            {
                ClassObject obj5 = null;
                FunctionObject obj6 = null;
                int num6 = 0;
                IntegerList list4 = new IntegerList(false);
                IntegerList list5 = new IntegerList(true);
                list5.Add(this.r.arg2);
                list5.Add(this.r.arg1);
                list5.Add(this.symbol_table.TRUE_id);
                IntegerList list6 = new IntegerList(true);
                list6.Add(0);
                list6.Add(0);
                list6.Add(0);
                if (operator_name == str2)
                {
                    MethodInfo info2 = (MethodInfo) this.scripter.OperatorHelpers[str2];
                    Type t = info2.ReflectedType;
                    int num7 = this.symbol_table.RegisterType(t, false);
                    int num8 = this.symbol_table.RegisterMethod(info2, num7);
                    obj5 = this.scripter.GetClassObject(num7);
                    FunctionObject f = this.scripter.GetFunctionObject(num8);
                    obj5.AddApplicableMethod(f, list5, list6, num6, ref obj6, ref list4);
                }
                num = 0;
                if (obj5 != null)
                {
                    obj5.CompressApplicableMethodList(list5, list4);
                    if (list4.Count >= 1)
                    {
                        num = list4[0];
                    }
                }
                if (num != 0)
                {
                    this.r.op = this.OP_NOP;
                    int num9 = this.r.res;
                    this.n++;
                    this.InsertOperators(this.n, 5);
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = list5[0];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = list5[1];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = list5[2];
                    this[this.n].arg2 = 0;
                    this[this.n].res = num;
                    this.n++;
                    this[this.n].op = this.OP_PUSH;
                    this[this.n].arg1 = obj5.Id;
                    this[this.n].arg2 = 0;
                    this[this.n].res = 0;
                    this.n++;
                    this[this.n].op = this.OP_CALL_SIMPLE;
                    this[this.n].arg1 = num;
                    this[this.n].arg2 = 2;
                    this[this.n].res = num9;
                    this.symbol_table[this[this.n].res].TypeId = this.symbol_table[num].TypeId;
                    return true;
                }
            }
            num = c1.FindOverloadableBinaryOperatorId(operator_name, this.r.arg1, this.r.arg2);
            if (num != 0)
            {
                this.r.op = this.OP_NOP;
                int num10 = this.r.res;
                this.n++;
                this.InsertOperators(this.n, 4);
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = this.r.arg1;
                this[this.n].arg2 = 0;
                this[this.n].res = num;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = this.r.arg2;
                this[this.n].arg2 = 0;
                this[this.n].res = num;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = c1.Id;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_CALL_SIMPLE;
                this[this.n].arg1 = num;
                this[this.n].arg2 = 2;
                this[this.n].res = num10;
                this.symbol_table[this[this.n].res].TypeId = this.symbol_table[num].TypeId;
                return true;
            }
            num = c2.FindOverloadableBinaryOperatorId(operator_name, this.r.arg2, this.r.arg1);
            if (num != 0)
            {
                this.r.op = this.OP_SWAPPED_ARGUMENTS;
                int num11 = this.r.res;
                this.n++;
                this.InsertOperators(this.n, 5);
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = this.r.arg2;
                this[this.n].arg2 = 0;
                this[this.n].res = num;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = this.r.arg1;
                this[this.n].arg2 = 0;
                this[this.n].res = num;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = c1.Id;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_CALL_SIMPLE;
                this[this.n].arg1 = num;
                this[this.n].arg2 = 2;
                this[this.n].res = num11;
                this.symbol_table[this[this.n].res].TypeId = this.symbol_table[num].TypeId;
                this.r.arg1 = this.symbol_table.TRUE_id;
                this.n++;
                this[this.n].op = this.OP_SWAPPED_ARGUMENTS;
                this[this.n].arg1 = this.symbol_table.FALSE_id;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                return true;
            }
            return false;
        }

        private bool TryOverloadableUnaryOperator(string operator_name, ClassObject c1)
        {
            int num = c1.FindOverloadableUnaryOperatorId(operator_name, this.r.arg1);
            if (num != 0)
            {
                this.r.op = this.OP_NOP;
                int res = this.r.res;
                this.n++;
                this.InsertOperators(this.n, 3);
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = this.r.arg1;
                this[this.n].arg2 = 0;
                this[this.n].res = num;
                this.n++;
                this[this.n].op = this.OP_PUSH;
                this[this.n].arg1 = c1.Id;
                this[this.n].arg2 = 0;
                this[this.n].res = 0;
                this.n++;
                this[this.n].op = this.OP_CALL_SIMPLE;
                this[this.n].arg1 = num;
                this[this.n].arg2 = 1;
                this[this.n].res = res;
                this.symbol_table[this[this.n].res].TypeId = this.symbol_table[num].TypeId;
            }
            return (num != 0);
        }

        private bool TryUnaryOperator(int type_id, ClassObject c1)
        {
            ClassObject classObject = this.GetClassObject(type_id);
            if (this.scripter.MatchTypes(c1, classObject))
            {
                if (c1.Class_Kind == ClassKind.Enum)
                {
                    this.symbol_table[this.r.res].TypeId = c1.Id;
                }
                else
                {
                    this.symbol_table[this.r.res].TypeId = classObject.Id;
                }
                return true;
            }
            return false;
        }

        public BreakpointList Breakpoints
        {
            get
            {
                return this.breakpoint_list;
            }
        }

        public CallStack Call_Stack
        {
            get
            {
                return this.callstack;
            }
        }

        public int Card
        {
            get
            {
                return this.card;
            }
            set
            {
                while (value >= this.prog.Count)
                {
                    for (int i = 0; i < 0x3e8; i++)
                    {
                        this.prog.Add(new ProgRec());
                    }
                }
                this.card = value;
            }
        }

        public string CurrentLine
        {
            get
            {
                int n = this.n;
                if (n == 0)
                {
                    n = this.Card;
                }
                 Module module = this.GetModule(n);
                if (module == null)
                {
                    return "";
                }
                int lineNumber = this.GetLineNumber(n);
                if (lineNumber == -1)
                {
                    return "";
                }
                return module.GetLine(lineNumber);
            }
        }

        public int CurrentLineNumber
        {
            get
            {
                int n = this.n;
                if (n == 0)
                {
                    n = this.Card;
                }
                return this.GetLineNumber(n);
            }
        }

        public string CurrentModule
        {
            get
            {
                int n = this.n;
                if (n == 0)
                {
                    n = this.Card;
                }
                 Module module = this.GetModule(n);
                if (module == null)
                {
                    return "";
                }
                return module.Name;
            }
        }

        public ProgRec this[int i]
        {
            get
            {
                return (ProgRec) this.prog[i];
            }
        }

        public int ObjectTypeId
        {
            get
            {
                return this.symbol_table.OBJECT_CLASS_id;
            }
        }

        private delegate void Oper();
    }
}

